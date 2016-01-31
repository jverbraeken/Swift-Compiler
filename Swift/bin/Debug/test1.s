  .file	"test1.swift"
  .def	___main;	.scl	2;	.type	32;	.endef
  .section .rdata,"dr"
LC0:
  .ascii "Goodbye\0"
  .text
  .globl	_main
  .def	_main;	.scl	2;	.type	32;	.endef
_main:
  pushl	%ebp
  movl	%esp, %ebp
  andl	$-16, %esp
  subl	$16, %esp
  call	___main
  movl	$LC0, (%esp)
  call	_puts
  leave
  ret
  .ident	"Yontu: (Joost Verbraeken) BETA"
