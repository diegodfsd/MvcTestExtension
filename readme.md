Projeto que mostra como � simples criar m�todos de extens�o para tornar seus testes mais interessantes.

> Exemplo:

`userController
	.Update()
	.ReturnsRedirectToRouteResult()
	.ToAction<UserController>(u => u.Show());`