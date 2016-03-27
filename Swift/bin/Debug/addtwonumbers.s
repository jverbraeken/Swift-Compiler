	.file	"addtwonumbers.c"
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC0:
	.ascii "Hello World!\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$32, %rsp
	call	__main
	leaq	.LC0(%rip), %rcx
	call	printf
	nop
	leave
	ret
	.ident	"GCC: (x86_64-posix-seh-rev0, Built by MinGW-W64 project) 5.3.0"
	.def	printf;	.scl	2;	.type	32;	.endef
