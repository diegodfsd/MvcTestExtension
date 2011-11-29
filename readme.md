Projeto que mostra como eh simples criar helpers para tornar o codigo dos seus testes legivel.

> Exemplo:

`userController
	.Update()
	.ReturnsRedirectToRouteResult()
	.ToAction<UserController>(u => u.Show());`