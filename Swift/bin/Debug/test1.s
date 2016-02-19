    .file	"test1.swift"
    .section	.rdata,"dr"
A0:
    .asciz "Hello World!"
    .text
    .globl	_main
    .def	_main;	.scl	2;	.type	32;		.endef
_main:
    pushl	%ebp
    movl	%esp, %ebp
    andl	$-16, %esp
    subl	$16, %esp
    call	___main
    movl	$A0, (%esp)
    call	_puts
    leave
    ret
    .ident	"Yontu: (Joost Verbraeken) BETA"
