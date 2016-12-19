#include "Common.hpp"

namespace mesosnet {

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
}
}
