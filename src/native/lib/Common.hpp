#ifndef COMMON_HPP_
#define COMMON_HPP_

#include <google/protobuf/message.h>

namespace mesosclr {

class Array {
public:
	int Length;
	void *Items;
};

class ByteArray {
public:
	~ByteArray();

	int Size;
	const char *Data;
};

ByteArray StringToByteArray(std::string string);

namespace protobuf {

ByteArray* SerializeToArray(const google::protobuf::Message& message);

}

}

#endif /* COMMON_HPP_ */
