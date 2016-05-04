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
	.def	__main;	.scl	2;	.type	32;	.endef
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
main:
	pushq	%rbp
	movq	%rsp, %rbp
	subq	$320, %rsp
	call	__main
	movabsq	$7238236157302825288, %rax
	movq	%rax, -32(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -24(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -16(%rbp)
	movl	$1953719668, -8(%rbp)
	movw	$12576, -4(%rbp)
	movb	$0, -2(%rbp)
	movabsq	$7238236157302825288, %rax
	movq	%rax, -64(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -56(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -48(%rbp)
	movl	$1953719668, -40(%rbp)
	movw	$12832, -36(%rbp)
	movb	$0, -34(%rbp)
	movabsq	$7238236157302825288, %rax
	movq	%rax, -96(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -88(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -80(%rbp)
	movl	$1953719668, -72(%rbp)
	movw	$13088, -68(%rbp)
	movb	$0, -66(%rbp)
	movabsq	$7238236157302825288, %rax
	movq	%rax, -128(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -120(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -112(%rbp)
	movl	$1953719668, -104(%rbp)
	movw	$13344, -100(%rbp)
	movb	$0, -98(%rbp)
	movabsq	$7238236157302825288, %rax
	movq	%rax, -160(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -152(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -144(%rbp)
	movl	$1953719668, -136(%rbp)
	movw	$13600, -132(%rbp)
	movb	$0, -130(%rbp)
	movabsq	$7238236157302825288, %rax
	movq	%rax, -192(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -184(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -176(%rbp)
	movl	$1953719668, -168(%rbp)
	movw	$13856, -164(%rbp)
	movb	$0, -162(%rbp)
	movabsq	$7238236157302825288, %rax
	movq	%rax, -224(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -216(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -208(%rbp)
	movl	$1953719668, -200(%rbp)
	movw	$14112, -196(%rbp)
	movb	$0, -194(%rbp)
	movabsq	$7238236157302825288, %rax
	movq	%rax, -256(%rbp)
	movabsq	$7379540993474261350, %rax
	movq	%rax, -248(%rbp)
	movabsq	$2334663869381960042, %rax
	movq	%rax, -240(%rbp)
	movl	$1953719668, -232(%rbp)
	movw	$14368, -228(%rbp)
	movb	$0, -226(%rbp)
	leaq	-128(%rbp), %r9
	leaq	-96(%rbp), %r8
	leaq	-64(%rbp), %rdx
	leaq	-32(%rbp), %rax
	leaq	-256(%rbp), %rcx
	movq	%rcx, 56(%rsp)
	leaq	-224(%rbp), %rcx
	movq	%rcx, 48(%rsp)
	leaq	-192(%rbp), %rcx
	movq	%rcx, 40(%rsp)
	leaq	-160(%rbp), %rcx
	movq	%rcx, 32(%rsp)
	movq	%rax, %rcx
	call	test
	nop
	leave
	ret
	.ident	"GCC: (x86_64-posix-seh-rev0, Built by MinGW-W64 project) 5.3.0"
	.def	strcpy;	.scl	2;	.type	32;	.endef
