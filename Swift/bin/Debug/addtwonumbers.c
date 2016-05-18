#include <stdio.h>
#include <string.h>

void test(const char *str1, const char *str2, const char *str3, const char *str4, const char *str5, const char *str6, const char *str7, const char *str8) {
	char tmp1[3];
	char tmp2[3];
	strcpy(tmp1, str1);
	strcpy(tmp2, str2);
}

void test2(char *str1, char *str2, char *str3, char *str4, char *str5, char *str6) {
	printf(str1);
	printf(str2);
	printf(str3);
	printf(str4);
	printf(str5);
	printf(str6);
}

void main() {
	/*const char str1[] = "This is a string to test if a function of the Intermediate Code Generator, namely AsciiToInt(), works correctly.";
	const char str2[] = "1";
	const char str3[] = "Halloasdfqwaoeifjaslkdf test 3";
	const char str4[] = "Halloasdfqwaoeifjaslkdf test 4";
	const char str5[] = "Halloasdfqwaoeifjaslkdf test 5";
	const char str6[] = "Halloasdfqwaoeifjaslkdf test 6";
	const char str7[] = "Halloasdfqwaoeifjaslkdf test 7";
	const char str8[] = "Halloasdfqwaoeifjaslkdf test 8";
	test(str1, str2, str3, str4, str5, str6, str7, str8);*/
	char str[80];
	strcpy(str, "Hello");
	printf(str);
	test2("1", "2", "3", "4", "5", "6");
}