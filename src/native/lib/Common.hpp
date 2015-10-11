#ifndef COMMON_HPP_
#define COMMON_HPP_

#include <google/protobuf/message.h>

namespace mesosclr {

class Collection {
public:
	int Size;
	void **Items;
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

template<class T>
Collection* SerializeVector(const std::vector<T>& items);

}

}

#endif /* COMMON_HPP_ */
