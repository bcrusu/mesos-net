#ifndef COMMON_HPP_
#define COMMON_HPP_

namespace mesosclr {

class Array {
public:
	int Length;
	void *Items;
};

class ByteArray {
public:
	int Length;
	unsigned char *Data;
};

}

#endif /* COMMON_HPP_ */
