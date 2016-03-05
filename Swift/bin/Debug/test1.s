    .file	"addtwonumbers.swift"
    .text
    .globl	main
main:
    pushq	%rbp
    movq	%rsp, %rbp
    subq	$32, %rsp
    subq	$24, %rsp
    call	__main
    pushq	$5
    popq	%rax
    movq	%rax, -8(%rbp)
    pushq	$10
    popq	%rax
    movq	%rax, -16(%rbp)
    movq	-8(%rbp), %rax
    pushq	%rax
    movq	-16(%rbp), %rax
    pushq	%rax
    popq	%rdx
    popq	%rax
    addq	%rdx, %rax
    pushq	%rax
    popq	%rax
    movq	%rax, -24(%rbp)
    nop
    movq	%rbp, %rsp
    popq	%rbp
    ret
    .ident	"Yontu: (x86_64-posix-seh-rev0, Built by Joost Verbraeken) BETA"
