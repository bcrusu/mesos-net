#include "Common.hpp"
#include <mesos/mesos.hpp>
#include <google/protobuf/io/zero_copy_stream_impl.h>

namespace mesosclr {

ByteArray::~ByteArray() {
	delete[] Data;
}

ByteArray StringToByteArray(const std::string& str) {
	ByteArray result;
	result.Size = str.length();
	result.Data = str.c_str();
	return result;
}

namespace protobuf {

ByteArray* Serialize(const google::protobuf::Message& message) {
	int size = message.ByteSize();
	char *buffer = new char[size];

	message.SerializeToArray(buffer, size);

	ByteArray* result = new ByteArray;
	result->Size = size;
	result->Data = buffer;
	return result;
}

template<class T>
Collection* SerializeVector(const std::vector<T>& items) {
	typedef typename std::vector<T>::size_type vector_size_type;

	Collection* result = new Collection;
	result->Size = items.size();
	ByteArray** offerBytesArray = new ByteArray*[items.size()];

	for (vector_size_type i = 0; i < items.size(); i++) {
		T item = items[i];
		offerBytesArray[i] = Serialize(item);
	}
	result->Items = (void**) offerBytesArray;

	return result;
}

template<class T>
T Deserialize(ByteArray* bytes) {
	google::protobuf::io::ArrayInputStream stream(bytes->Data, bytes->Size);
	T t;
	bool parsed = t.ParseFromZeroCopyStream(&stream);
	assert(parsed);
	return t;
}

template<class T>
std::vector<T> DeserializeVector(Collection* collection) {
	int size = collection->Size;
	ByteArray** items = (ByteArray**) collection->Items;

	std::vector<T> result = new std::vector<T>(size);
	for (int i = 0; i < size; i++) {
		ByteArray* itemBytes = items[i];
		T item = Deserialize<T>(itemBytes);
		result.push_back(item);
	}

	return result;
}

}
}
