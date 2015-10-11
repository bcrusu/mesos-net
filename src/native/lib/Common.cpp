#include "Common.hpp"
#include <google/protobuf/io/zero_copy_stream_impl.h>

namespace mesosclr {

ScopedByteArray::ScopedByteArray(ByteArray* byteArray) {
	_byteArray = byteArray;
}

ScopedByteArray::~ScopedByteArray() {
	delete[] _byteArray->Data;
	delete _byteArray;
}

ByteArray* ScopedByteArray::Ptr() {
	return _byteArray;
}

ScopedByteArrayCollection::ScopedByteArrayCollection(ByteArrayCollection* byteArrayCollection) {
	_byteArrayCollection = byteArrayCollection;
}

ScopedByteArrayCollection::~ScopedByteArrayCollection() {
	int size = _byteArrayCollection->Size;
	ByteArray** items = _byteArrayCollection->Items;
	for (int i = 0; i < size; i++) {
		ByteArray* item = items[i];
		delete[] item->Data;
		delete item;
	}

	delete _byteArrayCollection;
}

ByteArrayCollection* ScopedByteArrayCollection::Ptr() {
	return _byteArrayCollection;
}

ByteArray StringToByteArray(const std::string& str) {
	ByteArray result;
	result.Size = str.length();
	result.Data = str.c_str();
	return result;
}

namespace protobuf {

ScopedByteArray Serialize(const google::protobuf::Message& message) {
	int size = message.ByteSize();
	char *buffer = new char[size];

	message.SerializeToArray(buffer, size);

	ByteArray* byteArray = new ByteArray;
	byteArray->Size = size;
	byteArray->Data = buffer;

	ScopedByteArray result(byteArray);
	return result;
}

template<class T>
ScopedByteArrayCollection SerializeVector(const std::vector<T>& vector) {
	typedef typename std::vector<T>::size_type vector_size_type;

	ByteArrayCollection* collection = new ByteArrayCollection;
	collection->Size = vector.size();
	ByteArray** items = new ByteArray*[vector.size()];

	for (vector_size_type i = 0; i < vector.size(); i++)
		items[i] = Serialize(vector[i]);

	collection->Items = items;

	ScopedByteArrayCollection result(collection);
	return result;
}

template<class T>
T Deserialize(ByteArray* bytes) {
	google::protobuf::io::ArrayInputStream stream(bytes->Data, bytes->Size);
	T result;
	bool parsed = result.ParseFromZeroCopyStream(&stream);
	assert(parsed);
	return result;
}

template<class T>
std::vector<T> DeserializeVector(ByteArrayCollection* collection) {
	int size = collection->Size;
	ScopedByteArray** items = (ScopedByteArray**) collection->Items;

	std::vector<T> result = new std::vector<T>(size);
	for (int i = 0; i < size; i++) {
		ScopedByteArray* itemBytes = items[i];
		T item = Deserialize<T>(itemBytes);
		result.push_back(item);
	}

	return result;
}

}
}
