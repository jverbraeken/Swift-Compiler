#include <stdio.h>
#include <string.h>

void test(char *str1, char *str2, char *str3, char *str4, char *str5, char *str6, char *str7, char *str8) {
	char tmp1[3];
	char tmp2[3];
	strcpy(tmp1, str1);
	strcpy(tmp2, str2);
}

void main() {
	char str1[] = "Halloasdfqwaoeifjaslkdf test 1";
	char str2[] = "Halloasdfqwaoeifjaslkdf test 2";
	char str3[] = "Halloasdfqwaoeifjaslkdf test 3";
	char str4[] = "Halloasdfqwaoeifjaslkdf test 4";
	char str5[] = "Halloasdfqwaoeifjaslkdf test 5";
	char str6[] = "Halloasdfqwaoeifjaslkdf test 6";
	char str7[] = "Halloasdfqwaoeifjaslkdf test 7";
	char str8[] = "Halloasdfqwaoeifjaslkdf test 8";
	test(str1, str2, str3, str4, str5, str6, str7, str8);
}