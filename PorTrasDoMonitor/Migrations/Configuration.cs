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
                new Utilizadores {Avatar="default.jpg",ID=1,Descricao="Administrador da Aplicaçao web",Nome="Carlos Poupado",Username="admin@ipt.pt",DataNascimento= new DateTime(1995, 2, 22) },
                new Utilizadores { Avatar = "default.jpg", ID = 2, Descricao = "Jornalista da Aplicaçao web", Nome = "António Silva", Username = "Jornalista@ipt.com", DataNascimento = new DateTime(1970, 2, 09) },
                new Utilizadores {ID=3,Nome="Joao Fonseca",DataNascimento=new DateTime(1995,2,20),Descricao="Jornalista da pagina web",Avatar="default.jpg",Username="teste@ipt.pt"}
              
            };
            utilizadores.ForEach(aa => context.Utilizadores.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();

            //criação das noticias
            var noticias = new List<Noticias>
            {
                new Noticias{
                    ID =1,
                    Data=new DateTime(2019,06,28),
                    Titulo ="Chega no próximo ano, Cyberpunk 2077",
                    Capa="Cyberpunk.jpg",
                    UtilizadorFK=1,
                    Descricao="É um dos mais desejados entre os jogadores das várias plataformas.",
                    Conteudo="Só chega no próximo ano, mas o próximo jogo da CD Projekt Red, Cyberpunk 2077, é um dos mais desejados entre os jogadores das várias plataformas, conquistando os principais prémios da última E3 e deixando o mundo boquiaberto ao introduzir Keanu Reeves como Johnny Silverhand. O projeto é enorme, e por isso as informações sobre ele têm sido cirurgicamente introduzidas pelo estúdio polaco. As mais recentes têm a ver com uma característica do sistema de progressão das armas, que funciona de modo independente para cada tipo, e é mais do que um mero acumular numérico de poder. Cyberpunk 2077 vai ser lançado em abril do próximo ano, para PlayStation 4, Xbox One e PC. Se ainda não o fizeram, convidamos-vos a conferir a entrevista que conduzimos com Keanu Reeves, a propósito da sua aventura no jogo da CDPR.",
                    ListaCategorias = new List<Categorias>{categorias[0],categorias[1],categorias[2]},
                    ListaComentarios= new List<Comentarios>{ }
                },
                 new Noticias{
                    ID =2,
                    Data=new DateTime(2019,3,20),
                    Titulo ="PlayStation4 com Nova Atualização",
                    Capa="PlayStation4.jpg",
                    UtilizadorFK=1,
                    Descricao="Update de firmware 6.71!",
                    Conteudo="Os utilizadores da PlayStation 4 têm hoje uma nova atualização para a sua consola, o update de firmware 6.71, depois de ainda na última semana terem recebido o 6.70. Como a nova atualização refere apenas a 'melhoria da performance do sistema', é provável que venha apenas efetuar ligeira correções ao que foi feito na atualização anterior, no entanto, o facto de 'pesar' 463.9 MB, praticamente o mesmo da 4.70, é estranho e sugere que há algo a ser preparado, agora que nos aproximamos do final da vida da consola líder da atual geração.",
                    ListaCategorias = new List<Categorias>{categorias[2]},
                    ListaComentarios= new List<Comentarios>{ }
                },
                new Noticias{
                    ID =3,
                    Data=new DateTime(2019,6,28),
                    Titulo ="Teste Técnico de Gears 5!",
                    Capa="Gears5.jpg",
                    UtilizadorFK=1,
                    Descricao="Estará disponível para os subscritores do Xbox Game Pass.",
                    Conteudo="O teste técnico de Gears 5 estará disponível para os subscritores do Xbox Game Pass, para além dos jogadores que pré-adquirirem o jogo, terá confirmado a The Coalition segundo o Eurogamer. O download do teste técnico poderá ser transferido a partir do próximo dia 17 de julho, mas será necessário uma subscrição ativa do Xbox Live Gold.Os membros do Xbox Game Pass poderão vê-lo na sua biblioteca, tal como os restantes jogos do serviço. Este teste estará disponível desde o dia 19 de julho pelas 18h00 de Portugal continental até ao dia 21 do mesmo mês. Depois será reativado no dia 26 de julho pelas 18h00 de Portugal continental, até ao dia 29 de julho.",
                    ListaCategorias = new List<Categorias>{categorias[0],categorias[1] },
                    ListaComentarios= new List<Comentarios>{ }
                },
                 new Noticias{
                    ID =4,
                    Data=new DateTime(2019,6,27),
                    Titulo ="Pokemon SWORD e SHIELD terão Pokemons gigantes.",
                    Capa="Pokemon.jpeg",
                    UtilizadorFK=1,
                    Descricao="A Game Freak mostrou-nos uma nova mecânica.",
                    Conteudo="Durante a nova Nintendo Direct totalmente dedicada a Pokemon Sword e Pokemon Shield, a Game Freak mostrou-nos uma nova mecânica. Através de um pequeno vídeo, que nos mostrou algumas localizações da nova região e nos revelou que os Pokemon andarão livremente no mundo, ficamos a saber que, de alguma forma, vamos poder transformas as nossas criaturas em seres gigantes.",
                    ListaCategorias = new List<Categorias>{categorias[3]},
                    ListaComentarios= new List<Comentarios>{ }
                },
                  new Noticias{
                    ID =5,
                    Data=new DateTime(2019,07,07),
                    Titulo ="COLDZERA PEDE PARA SAIR DA MIBR - PROCURA NOVO DESAFIO",
                    Capa="coldzera.jpg",
                    UtilizadorFK=1,
                    Descricao="Para além disso, os números estatísticos de 2019 de coldzera estão muito aquém quando comparados com os seus anteriores, revelando que estes 'problemas internos' afetaram a confiança e jogo do jogador de 24 anos.",
                    Conteudo="De acordo com a HLTV.org, o melhor jogador do mundo em 2016 e 2017, Marcelo 'coldzera' David, pediu à organização da MIBR para ser colocado no banco da equipa de Counter-Strike: Global Offensive, pois procura um novo desafio para a sua carreira. O conjunto brasileiro foi eliminado da ESL One Cologne 2019 cedo depois de derrotas frente aos fnatic e BIG naquele que foi o seu primeiro evento com Lucas 'LUCAS1' Teles, que chegou no dia 25 de junho por empréstimo da Luminosity para o lugar de João 'felps' Vasconcellos. A HLTV.org adianta que coldzera está 'frustrado com os pobres resultados' que têm obtido, sendo que este ano ainda não conseguiram chegar a nenhuma final e é evidente que a saída de felps não foi suficiente para corrigir os 'problemas internos' existentes na atual 12ª melhor equipa do mundo. Caso isto se confirme, Wilton 'zews' Prado será obrigado a colmatar a ausência da estrela brasileira no StarLadder Major 2019 em setembro, embora coldzera ainda alinhará pela equipa de Gabriel 'FalleN' Toledo em mais dois torneios (BLAST Pro Series Los Angeles 2019 e IEM Chicago 2019) antes de deixar o roster ativo.",
                    ListaCategorias = new List<Categorias>{categorias[4],categorias[0]},
                    ListaComentarios= new List<Comentarios>{ }
                }
            };
            noticias.ForEach(aa => context.Noticias.AddOrUpdate(a => a.Data, aa));
            context.SaveChanges();

            //criação de comentários
            var comentarios = new List<Comentarios>
            {
                new Comentarios{ID=1,Descricao="Comentário de teste",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=1},
                new Comentarios{ID=2,Descricao="Comentário de teste",Data=new DateTime(2019,6,28),UtilizadorFK=2,NoticiasFK=1},
                new Comentarios{ID=3,Descricao="Outro comentário",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=1},
                new Comentarios{ID=4,Descricao="Comentário novo",Data=new DateTime(2019,6,28),UtilizadorFK=1,NoticiasFK=2},
                 new Comentarios{ID=5,Descricao="Outro comentário",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=2},
                new Comentarios{ID=6,Descricao="Outro comentário",Data=new DateTime(2019,6,28),UtilizadorFK=3,NoticiasFK=2}


            };
            comentarios.ForEach(aa => context.Comentarios.AddOrUpdate(a => a.Descricao, aa));
            context.SaveChanges();
        }
    }
}