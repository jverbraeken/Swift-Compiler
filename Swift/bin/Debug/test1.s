    .file	"addtwonumbers.swift"
    .text
    .globl	main
main:
    pushq	%rbp
    movq	%rsp, %rbp
    subq	$32, %rsp
    subq	$8, %rsp
    call	__main
    movq	$1, -8(%rbp)
    nop
    movq	%rbp, %rsp
    popq	%rbp
    ret
    .ident	"Yontu: (x86_64-posix-seh-rev0, Built by Joost Verbraeken) BETA"
