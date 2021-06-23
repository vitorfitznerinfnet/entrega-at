#language: pt-br
Funcionalidade: Cadastrar Aluno

Cenário: Cadastrar um aluno com nome e email
Dado que acessei a página de cadastro de alunos
E informei o nome 'vitor'
E informei o email 'vitor@teste.com'
Quando clicar no botão de cadastrar
Então devo ser redirecionado para a página de lista de alunos
E um aluno com o nome 'vitor' e email 'vitor@teste.com' deve existir na página