Projeto que mostra como é simples criar métodos de extensão para tornar seus testes mais interessantes.

> Exemplo:

`userController
	.Update()
	.ReturnsRedirectToRouteResult()
	.ToAction<UserController>(u => u.Show());`