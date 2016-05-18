	.file	"addtwonumbers.c"
	.text
	.globl	test
	.def	test;	.scl	2;	.type	32;	.endef
test:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$64, %rsp
	movq	%rcx, 16(%rbp)
	movq	%rdx, 24(%rbp)
	movq	%r8, 32(%rbp)
	movq	%r9, 40(%rbp)
	leaq	-16(%rbp), %rax
	movq	16(%rbp), %rdx
	movq	%rax, %rcx
	call	strcpy
	movq	24(%rbp), %rdx
	leaq	-32(%rbp), %rax
	movq	%rax, %rcx
	call	strcpy
	nop
	leave
	ret
	.globl	test2
	.def	test2;	.scl	2;	.type	32;	.endef
test2:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$32, %rsp
	movq	%rcx, 16(%rbp)
	movq	%rdx, 24(%rbp)
	movq	%r8, 32(%rbp)
	movq	%r9, 40(%rbp)
	movq	16(%rbp), %rcx
	call	printf
	movq	24(%rbp), %rax
	movq	%rax, %rcx
	call	printf
	movq	32(%rbp), %rax
	movq	%rax, %rcx
	call	printf
	movq	40(%rbp), %rax
	movq	%rax, %rcx
	call	printf
	movq	48(%rbp), %rax
	movq	%rax, %rcx
	call	printf
	movq	56(%rbp), %rax
	movq	%rax, %rcx
	call	printf
	nop
	leave
	ret
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC0:
	.ascii "4\0"
.LC1:
	.ascii "3\0"
.LC2:
	.ascii "2\0"
.LC3:
	.ascii "1\0"
.LC4:
	.ascii "6\0"
.LC5:
	.ascii "5\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	addq	$-128, %rsp
	call	__main
	leaq	-80(%rbp), %rax
	movl	$1819043144, (%rax)
	movw	$111, 4(%rax)
	leaq	-80(%rbp), %rax
	movq	%rax, %rcx
	call	printf
	leaq	.LC4(%rip), %rax
	movq	%rax, 40(%rsp)
	leaq	.LC5(%rip), %rax
	movq	%rax, 32(%rsp)
	leaq	.LC0(%rip), %r9
	leaq	.LC1(%rip), %r8
	leaq	.LC2(%rip), %rdx
	leaq	.LC3(%rip), %rcx
	call	test2
	nop
	leave
	ret
	.ident	"GCC: (x86_64-posix-seh-rev0, Built by MinGW-W64 project) 5.3.0"
	.def	strcpy;	.scl	2;	.type	32;	.endef
	.def	printf;	.scl	2;	.type	32;	.endef
