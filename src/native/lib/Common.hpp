#ifndef COMMON_HPP_
#define COMMON_HPP_

#include <google/protobuf/message.h>

namespace mesosclr {

struct ByteArray {
public:
	int Size;
	const char *Data;
};

struct ByteArrayCollection {
public:
	int Size;
	ByteArray **Items;
};

class ScopedByteArray {
public:
	ScopedByteArray(ByteArray* byteArray);
	~ScopedByteArray();

	ByteArray* Ptr();

private:
	ByteArray* _byteArray;
};

class ScopedByteArrayCollection {
public:
	ScopedByteArrayCollection(ByteArrayCollection* byteArrayCollection);
	~ScopedByteArrayCollection();

	ByteArrayCollection* Ptr();

private:
	ByteArrayCollection* _byteArrayCollection;
};

ByteArray StringToByteArray(std::string string);

namespace protobuf {

ScopedByteArray Serialize(const google::protobuf::Message& message);

template<class T>
ScopedByteArrayCollection SerializeVector(const std::vector<T>& items);

template<class T>
T Deserialize(ByteArray* bytes);

template<class T>
std::vector<T> DeserializeVector(ByteArrayCollection* collection);

}

}

#endif /* COMMON_HPP_ */
