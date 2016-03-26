    .file	"addtwonumbers.swift"
    .section	.rdata,"dr"
.LC0:
    .asciz	"%d"
    .text
    .globl	main
main:
    pushq	%rbp
    movq	%rsp, %rbp
    subq	$32, %rsp
    subq	$24, %rsp
    call	__main
    movq	$5, -8(%rbp)
    movq	$10, -16(%rbp)
    movq	-16(%rbp), %rdx
    movq	-8(%rbp), %rax
    addq	%rdx, %rax
    movq	%rax, -24(%rbp)
    movq	-24(%rbp), %rdx
    leaq	.LC0(%rip), %rcx
    call	printf
    nop
    movq	%rbp, %rsp
    popq	%rbp
    ret
    .ident	"Yontu: (x86_64-posix-seh-rev0, Built by Joost Verbraeken) BETA"
