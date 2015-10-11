#include "Common.hpp"

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

ByteArray* SerializeToArray(const google::protobuf::Message& message) {
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
		offerBytesArray[i] = SerializeToArray(item);
	}
	result->Items = (void**) offerBytesArray;

	return result;
}

}
}
