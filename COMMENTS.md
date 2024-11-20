# Decisão da arquitetura utilizada
** API
	- Utilizei NET 8 e para contrução da API utilzei MVC com injeção de dependencia, Alem de estar habituado a este metodo acredito que fique mais facil de se entender a estrutura do projeto, principalmente quando se trata de codigo legados, entao acaba facilitando para outros desenvolvedores entender a estrutura do projeto com mais facilidade.Alem disto Criei separadamente da API um Lib de Classes para que fique mais organizado possivel. Desta forma todos os Modelos do Bando de Dados e tambem modelos de apoio para API ficam em um so lugar assim facilitando a manutenção, com esta lib por exemplo poderia criar diversos projetos que poderiam ser referenciados em uma biblioteca só, isto acaba deixando o codigo mais limpo e sem precisar refazer modelos.

** DashBoard
	- Utilizei Vuetify 2.6 + Nuxt 2.15 por se tratar de versões que estou mais habituado a utilizar, Para o Front eu tambem uso o mesmo padrao da API para criar modelos de helpers de apoio para o front, ao inves de  criar funções especificar e unicas(principalmente para integração via API), uso modelos padroes para que eu possa replicar em todas as telas, a casos e casos mas no geral tento deixar o mais dinamico possivel.

** Banco de Dados
	- Utilizei o Mysql por ja ter bastante experiencia com ele.

# Lista de bibliotecas de terceiros utilizadas
- V-Mask
- SweetAlert


# O que você melhoraria se tivesse mais tempo

** Tratamento mais robusto para validar o JWT Token no front-end:
	- Implementei uma validação mais robusta para o JWT Token no front-end, verificando a autenticidade do token e sua expiração, para possibilitar a atualização do token (refreshToken).

** Adicionar níveis de permissão de usuários
	- Criei a tabela user para testar diferentes perfis, permitindo bloquear requisições e esconder itens do menu para usuários que não sejam admin, caso haja outros tipos de usuários além do admin. Porem não tive tempo habil para implementar.

** Adicionar os Testes Unitários
	- Iniciei a implementação dos testes unitários, mas, devido à falta de tempo, optei por não completá-los, mesmo tendo feito boa pate do processo.

** Adicionar validador de CPF no Back-End
	- Realizei a validação no front-end porem nao tive tempo para fazer no back-end.



# Quais requisitos obrigatórios que não foram entregues
Acredito todos os requisitos foram atendidos.

# Observações
Aqui é uma queria deixar registrado que por uma fatalidade na hora de criar o DashBoard eu acabei referenciando a outro diretorio do github, o problema em si nem seria este mas sim os commits, como tive varios principalmente no front-end acabei "perdendo" todos os commits do DashBoard, eu tenho eles localmente, mas no repositorio na hora de voces checarem voces vao observar que tera somente 1 commit e é por conta disto, mas segue os commits que realizei para o DashBoard em ordem Decresente:

-Adjust to open the home page correctly.
-Added V-Mask lib and Added function to validate CPF, Final Layout adjustments
-All Layout adjustments completed and the Student Listing integrated with the API
-Added integration on the Update Student screen, Created the Service to consume the Students' CRUD, Added the Deactivate Student function.
-Added API Integration in Student Creation, Remove the Console.Log from pages
-Added Integration with the Login/Logout/RefreshToken flow, Added the SweetAlert Lib, Added the Middleware for User validation.
-Layout adjustments and added login integration services, Added helpers and support services for API integration
-Added the main pages and added the styles for the pages.
-Added the main pages and added the styles for the pages.