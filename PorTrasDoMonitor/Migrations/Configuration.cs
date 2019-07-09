namespace PorTrasDoMonitor.Migrations
{
    using PorTrasDoMonitor.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using IdentitySample.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //Inicio do SEED

            //Categorias

            var categorias = new List<Categorias>
            {
                new Categorias{ID=1,TipoCategoria="Pc"},
                new Categorias{ID=2,TipoCategoria="Xboxone"},
                new Categorias{ID=3,TipoCategoria="Playstation"},
                new Categorias{ID=4,TipoCategoria="Switch"},
                new Categorias{ID=5,TipoCategoria="Esports"},
                new Categorias{ID=6,TipoCategoria="Outros"}
            };
            categorias.ForEach(aa => context.Categorias.AddOrUpdate(a => a.TipoCategoria, aa));
            context.SaveChanges();

            //Utilizadores
            var utilizadores = new List<Utilizadores> {
                new Utilizadores {Avatar="default.jpg",ID=1,Descricao="Administrador da Aplica�ao web",Nome="Carlos Poupado",Username="admin@ipt.pt",DataNascimento= new DateTime(1995, 2, 22) },
                new Utilizadores { Avatar = "default.jpg", ID = 2, Descricao = "Jornalista da Aplica�ao web", Nome = "Ant�nio Silva", Username = "Jornalista@ipt.com", DataNascimento = new DateTime(1970, 2, 09) },
                new Utilizadores {ID=3,Nome="Joao Fonseca",DataNascimento=new DateTime(1995,2,20),Descricao="Jornalista da pagina web",Avatar="default.jpg",Username="teste@ipt.pt"}
              
            };
            utilizadores.ForEach(aa => context.Utilizadores.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();

            //cria��o das noticias
            var noticias = new List<Noticias>
            {
                new Noticias{
                    ID =1,
                    Data=new DateTime(2019,06,28),
                    Titulo ="Chega no pr�ximo ano, Cyberpunk 2077",
                    Capa="Cyberpunk.jpg",
                    UtilizadorFK=1,
                    Descricao="� um dos mais desejados entre os jogadores das v�rias plataformas.",
                    Conteudo="S� chega no pr�ximo ano, mas o pr�ximo jogo da CD Projekt Red, Cyberpunk 2077, � um dos mais desejados entre os jogadores das v�rias plataformas, conquistando os principais pr�mios da �ltima E3 e deixando o mundo boquiaberto ao introduzir Keanu Reeves como Johnny Silverhand. O projeto � enorme, e por isso as informa��es sobre ele t�m sido cirurgicamente introduzidas pelo est�dio polaco. As mais recentes t�m a ver com uma caracter�stica do sistema de progress�o das armas, que funciona de modo independente para cada tipo, e � mais do que um mero acumular num�rico de poder. Cyberpunk 2077 vai ser lan�ado em abril do pr�ximo ano, para PlayStation 4, Xbox One e PC. Se ainda n�o o fizeram, convidamos-vos a conferir a entrevista que conduzimos com Keanu Reeves, a prop�sito da sua aventura no jogo da CDPR.",
                    ListaCategorias = new List<Categorias>{categorias[0],categorias[1],categorias[2]},
                    ListaComentarios= new List<Comentarios>{ }
                },
                 new Noticias{
                    ID =2,
                    Data=new DateTime(2019,3,20),
                    Titulo ="PlayStation4 com Nova Atualiza��o",
                    Capa="PlayStation4.jpg",
                    UtilizadorFK=1,
                    Descricao="Update de firmware 6.71!",
                    Conteudo="Os utilizadores da PlayStation 4 t�m hoje uma nova atualiza��o para a sua consola, o update de firmware 6.71, depois de ainda na �ltima semana terem recebido o 6.70. Como a nova atualiza��o refere apenas a 'melhoria da performance do sistema', � prov�vel que venha apenas efetuar ligeira corre��es ao que foi feito na atualiza��o anterior, no entanto, o facto de 'pesar' 463.9 MB, praticamente o mesmo da 4.70, � estranho e sugere que h� algo a ser preparado, agora que nos aproximamos do final da vida da consola l�der da atual gera��o.",
                    ListaCategorias = new List<Categorias>{categorias[2]},
                    ListaComentarios= new List<Comentarios>{ }
                },
                new Noticias{
                    ID =3,
                    Data=new DateTime(2019,6,28),
                    Titulo ="Teste T�cnico de Gears 5!",
                    Capa="Gears5.jpg",
                    UtilizadorFK=1,
                    Descricao="Estar� dispon�vel para os subscritores do Xbox Game Pass.",
                    Conteudo="O teste t�cnico de Gears 5 estar� dispon�vel para os subscritores do Xbox Game Pass, para al�m dos jogadores que pr�-adquirirem o jogo, ter� confirmado a The Coalition segundo o Eurogamer. O download do teste t�cnico poder� ser transferido a partir do pr�ximo dia 17 de julho, mas ser� necess�rio uma subscri��o ativa do Xbox Live Gold.Os membros do Xbox Game Pass poder�o v�-lo na sua biblioteca, tal como os restantes jogos do servi�o. Este teste estar� dispon�vel desde o dia 19 de julho pelas 18h00 de Portugal continental at� ao dia 21 do mesmo m�s. Depois ser� reativado no dia 26 de julho pelas 18h00 de Portugal continental, at� ao dia 29 de julho.",
                    ListaCategorias = new List<Categorias>{categorias[0],categorias[1] },
                    ListaComentarios= new List<Comentarios>{ }
                },
                 new Noticias{
                    ID =4,
                    Data=new DateTime(2019,6,27),
                    Titulo ="Pokemon SWORD e SHIELD ter�o Pokemons gigantes.",
                    Capa="Pokemon.jpeg",
                    UtilizadorFK=1,
                    Descricao="A Game Freak mostrou-nos uma nova mec�nica.",
                    Conteudo="Durante a nova Nintendo Direct totalmente dedicada a Pokemon Sword e Pokemon Shield, a Game Freak mostrou-nos uma nova mec�nica. Atrav�s de um pequeno v�deo, que nos mostrou algumas localiza��es da nova regi�o e nos revelou que os Pokemon andar�o livremente no mundo, ficamos a saber que, de alguma forma, vamos poder transformas as nossas criaturas em seres gigantes.",
                    ListaCategorias = new List<Categorias>{categorias[3]},
                    ListaComentarios= new List<Comentarios>{ }
                },
                  new Noticias{
                    ID =5,
                    Data=new DateTime(2019,07,07),
                    Titulo ="COLDZERA PEDE PARA SAIR DA MIBR - PROCURA NOVO DESAFIO",
                    Capa="coldzera.jpg",
                    UtilizadorFK=1,
                    Descricao="Para al�m disso, os n�meros estat�sticos de 2019 de coldzera est�o muito aqu�m quando comparados com os seus anteriores, revelando que estes 'problemas internos' afetaram a confian�a e jogo do jogador de 24 anos.",
                    Conteudo="De acordo com a HLTV.org, o melhor jogador do mundo em 2016 e 2017, Marcelo 'coldzera' David, pediu � organiza��o da MIBR para ser colocado no banco da equipa de Counter-Strike: Global Offensive, pois procura um novo desafio para a sua carreira. O conjunto brasileiro foi eliminado da ESL One Cologne 2019 cedo depois de derrotas frente aos fnatic e BIG naquele que foi o seu primeiro evento com Lucas 'LUCAS1' Teles, que chegou no dia 25 de junho por empr�stimo da Luminosity para o lugar de Jo�o 'felps' Vasconcellos. A HLTV.org adianta que coldzera est� 'frustrado com os pobres resultados' que t�m obtido, sendo que este ano ainda n�o conseguiram chegar a nenhuma final e � evidente que a sa�da de felps n�o foi suficiente para corrigir os 'problemas internos' existentes na atual 12� melhor equipa do mundo. Caso isto se confirme, Wilton 'zews' Prado ser� obrigado a colmatar a aus�ncia da estrela brasileira no StarLadder Major 2019 em setembro, embora coldzera ainda alinhar� pela equipa de Gabriel 'FalleN' Toledo em mais dois torneios (BLAST Pro Series Los Angeles 2019 e IEM Chicago 2019) antes de deixar o roster ativo.",
                    ListaCategorias = new List<Categorias>{categorias[4],categorias[0]},
                    ListaComentarios= new List<Comentarios>{ }
                }
            };
            noticias.ForEach(aa => context.Noticias.AddOrUpdate(a => a.Data, aa));
            context.SaveChanges();

            //cria��o de coment�rios
            var comentarios = new List<Comentarios>
            {
                new Comentarios{ID=1,Descricao="Coment�rio de teste",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=1},
                new Comentarios{ID=2,Descricao="Coment�rio de teste",Data=new DateTime(2019,6,28),UtilizadorFK=2,NoticiasFK=1},
                new Comentarios{ID=3,Descricao="Outro coment�rio",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=1},
                new Comentarios{ID=4,Descricao="Coment�rio novo",Data=new DateTime(2019,6,28),UtilizadorFK=1,NoticiasFK=2},
                 new Comentarios{ID=5,Descricao="Outro coment�rio",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=2},
                new Comentarios{ID=6,Descricao="Outro coment�rio",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=2}


            };
            comentarios.ForEach(aa => context.Comentarios.AddOrUpdate(a => a.Descricao, aa));
            context.SaveChanges();
        }
    }
}