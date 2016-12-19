#ifndef COMMON_HPP_
#define COMMON_HPP_

#include <google/protobuf/message.h>
#include <google/protobuf/io/zero_copy_stream_impl.h>

namespace mesosnet {

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

ByteArray StringToByteArray(const std::string& str);

namespace protobuf {

ScopedByteArray Serialize(const google::protobuf::Message& message);

template<class T>
ScopedByteArrayCollection SerializeVector(const std::vector<T>& vector) {
	typedef typename std::vector<T>::size_type vector_size_type;

	ByteArrayCollection* collection = new ByteArrayCollection;
	collection->Size = vector.size();
	ByteArray** items = new ByteArray*[vector.size()];

	for (vector_size_type i = 0; i < vector.size(); i++) {
		T item = vector[i];
		int size = item.ByteSize();
		char *buffer = new char[size];

		item.SerializeToArray(buffer, size);

		ByteArray* byteArray = new ByteArray;
		byteArray->Size = size;
		byteArray->Data = buffer;

		items[i] = byteArray;
	}

	collection->Items = items;

	ScopedByteArrayCollection result(collection);
	return result;
}

template<class T>
T Deserialize(ByteArray* bytes) {
	assert(bytes);
	google::protobuf::io::ArrayInputStream stream(bytes->Data, bytes->Size);
	T result;
	bool parsed = result.ParseFromZeroCopyStream(&stream);
	assert(parsed);
	return result;
}

template<class T>
std::vector<T> DeserializeVector(ByteArrayCollection* collection) {
	int size = collection->Size;
	ByteArray** items = (ByteArray**) collection->Items;

	std::vector<T> result;
	for (int i = 0; i < size; i++) {
		ByteArray* itemBytes = items[i];
		const T& item = Deserialize<T>(itemBytes);
		result.push_back(item);
	}

	return result;
}

}

}

#endif /* COMMON_HPP_ */
