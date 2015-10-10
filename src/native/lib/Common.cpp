#include "Common.hpp"

namespace mesosclr {

ByteArray::~ByteArray() {
	delete[] Data;
}

ByteArray StringToByteArray(const std::string& str){
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

}
}
