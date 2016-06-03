	.file	"addtwonumbers.c"
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC0:
	.ascii "Hello World!\0"
	.section	.text.unlikely,"x"
.LCOLDB1:
	.section	.text.startup,"x"
.LHOTB1:
	.p2align 4,,15
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	subq	$40, %rsp
	call	__main
	leaq	.LC0(%rip), %rcx
	addq	$40, %rsp
	jmp	printf
	.section	.text.unlikely,"x"
.LCOLDE1:
	.section	.text.startup,"x"
.LHOTE1:
	.ident	"GCC: (x86_64-posix-seh-rev0, Built by MinGW-W64 project) 5.3.0"
	.def	printf;	.scl	2;	.type	32;	.endef
