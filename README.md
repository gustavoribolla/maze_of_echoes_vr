# Maze of Echoes VR

Maze of Echoes VR é um jogo de labirinto em realidade virtual desenvolvido na Unity. O jogador explora um ambiente escuro e atmosférico usando uma lanterna, precisa gerenciar a bateria, encontrar pilhas pelo caminho e ativar uma alavanca para abrir o portão final do labirinto.

O projeto foi desenvolvido como parte da disciplina de Virtual Reality 2026.1.

## Sobre o jogo

O objetivo do jogo é escapar de um labirinto escuro e chuvoso. Durante a exploração, o jogador utiliza uma lanterna para se orientar, mas a bateria da lanterna é limitada. Para continuar avançando, é necessário encontrar baterias espalhadas pelo mapa.

Ao chegar na área final, o jogador deve ativar uma alavanca para abrir o portão e alcançar a zona de saída. Ao finalizar o percurso, o jogo exibe o tempo total gasto até a conclusão.

## Funcionalidades

- Ambiente 3D em estilo labirinto;
- Suporte a VR com Unity XR;
- Menu inicial e menu final em World Space;
- Sistema de lanterna com bateria;
- Coleta de baterias para recarregar a lanterna;
- Mensagens de feedback para o jogador;
- Alavanca interativa para abrir o portão final;
- Zona de finalização do jogo;
- Exibição do tempo final no menu de encerramento;
- Sons ambientes, chuva, passos, monstro e efeitos de interação;
- Modo de teste por teclado para desenvolvimento sem headset VR.

## Controles

### No VR

- Usar os controles do headset para movimentação;
- Usar o botão configurado no controle para ligar/desligar a lanterna;
- Interagir com menus, baterias e alavanca conforme os sistemas configurados no projeto.

### No modo de teste pelo PC

Este modo foi criado apenas para facilitar testes sem o óculos VR.

- `Enter`: iniciar/reiniciar o jogo;
- `WASD`: movimentar o jogador;
- `Q / E`: girar o jogador;
- `F`: ligar/desligar a lanterna;
- `G`: ativar a alavanca;
- `R`: reiniciar a cena;
- `Esc`: sair do Play Mode no Editor.

## Estrutura principal da cena

A cena principal está organizada em grupos para facilitar manutenção:

```txt
Main VR Scene
├── VR Player
├── Environment
│   ├── Lights
│   ├── Maze
│   ├── Scenario
│   └── Collectibles
├── Menus
│   ├── StartMenu
│   └── FinalArea
│       ├── FinalGate
│       ├── FinalGateController
│       ├── Lever
│       ├── FinishZone
│       └── EndMenu
└── Managers
    ├── XR Interaction Manager
    ├── GameManager
    ├── EventSystem
    └── AudioManager
````

## Sistemas implementados

### Sistema de lanterna

A lanterna possui bateria limitada. A bateria só diminui enquanto a lanterna está ligada. Quando acaba, a lanterna desliga e uma mensagem orienta o jogador a procurar uma bateria.

### Sistema de baterias

As baterias estão espalhadas pelo labirinto. Ao se aproximar de uma bateria, ela recarrega a lanterna e desaparece do mapa.

### Sistema de alavanca e portão

Na área final, o jogador encontra uma alavanca. Ao ativá-la, a porta final é removida, permitindo o acesso à `FinishZone`.

### Sistema de menus

O jogo possui um menu inicial e um menu final em World Space. O menu final exibe o tempo total que o jogador levou para completar o labirinto.

## Tecnologias utilizadas

* Unity 6;
* Universal Render Pipeline (URP);
* XR Interaction Toolkit;
* Input System;
* TextMeshPro;
* Meta Quest / Android build target.

## Como executar

1. Clone o repositório:

```bash
git clone <https://github.com/gustavoribolla/maze_of_echoes_vr.git>
```

2. Abra o projeto pelo Unity Hub.

3. Verifique se as dependências XR estão instaladas:

```txt
XR Plugin Management
XR Interaction Toolkit
Input System
TextMeshPro
```

4. Abra a cena principal do projeto.

5. Para testar no Editor, use o modo de teclado.

6. Para testar em VR, conecte o headset, configure o build para Android e execute no dispositivo.

## Observações de desenvolvimento

O projeto contém alguns sistemas de debug por teclado para facilitar o desenvolvimento sem o headset VR. Esses sistemas podem ser desativados no Inspector antes da versão final, desmarcando os campos de debug nos scripts correspondentes.


## Créditos

Desenvolvido por Gustavo Colombi Ribolla.
Projeto desenvolvido para a disciplina de Virtual Reality 2026.1.