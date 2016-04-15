    .file	"addtwonumbers.swift"
    .section	.rdata,"dr"
.LC0:
    .asciz	"hoi"
.LC1:
    .asciz	"%s"
    .text
    .globl	main
main:
    pushq	%rbp
    movq	%rsp, %rbp
    subq	$32, %rsp
    subq	$8, %rsp
    call	__main
    leaq	.LC0(%rip), %rax
    movq	%rax, -8(%rbp)
    movq	-8(%rbp), %rdx
    leaq	.LC1(%rip), %rcx
    call	printf
    nop
    movq	%rbp, %rsp
    popq	%rbp
    ret
    .ident	"Yontu: (x86_64-posix-seh-rev0, Built by Joost Verbraeken) BETA"
