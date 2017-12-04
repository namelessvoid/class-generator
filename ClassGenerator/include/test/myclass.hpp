#ifndef FOO_MYCLASS_HPP
#define FOO_MYCLASS_HPP

namespace foo {

class MyClass
{
public:
	MyClass();

	~MyClass();

private:
	MyClass(const MyClass& rhs) = delete;

	MyClass& operator=(const MyClass& rhs) = delete;
};


} // namespace foo

#endif // FOO_MYCLASS_HPP
