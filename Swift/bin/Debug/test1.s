    .file	"addtwonumbers.swift"
    .section	.rdata,"dr"
.LC0:
    .asciz	"Hello World!"
    .text
    .globl	main
main:
    pushq	%rbp
    movq	%rsp, %rbp
    subq	$32, %rsp
    subq	$0, %rsp
    call	__main
    leaq	.LC0(%rip), %rcx
    call	printf
    nop
    movq	%rbp, %rsp
    popq	%rbp
    ret
    .ident	"Yontu: (x86_64-posix-seh-rev0, Built by Joost Verbraeken) BETA"
