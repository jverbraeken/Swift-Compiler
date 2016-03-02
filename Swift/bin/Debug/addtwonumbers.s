	.file	"addtwonumbers.c"
	.def	__main;	.scl	2;	.type	32;	.endef
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$64, %rsp
	call	__main
	movl	$50, -4(%rbp)
	movl	$100, -8(%rbp)
	movl	$150, -12(%rbp)
	movl	$200, -16(%rbp)
	movl	$250, -20(%rbp)
	movl	$300, -24(%rbp)
	movl	-8(%rbp), %edx
	movl	-12(%rbp), %eax
	addl	%eax, %edx
	movl	-16(%rbp), %eax
	addl	%edx, %eax
	movl	%eax, -4(%rbp)
	nop
	leave
	ret
	.ident	"GCC: (x86_64-posix-seh-rev0, Built by MinGW-W64 project) 5.3.0"
