using System;
using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Vector2f = SFML.Window.Vector2f;

namespace Pong {
    public class MenuState : IState {
        private Game _game;
        private Text _play_text= new Text();
        private Text _Ai_text= new Text();
        private Text _title = new Text();
        public MenuState(Game game) {
            _game = game;
            Font font = new Font("./arial.ttf");
            _play_text.Font = font;
            _play_text.Color = new Color(Color.White);
            _play_text.Position = new Vector2f(250,200);
            _play_text.CharacterSize = 20;
            _play_text.DisplayedString = "Press Q to Play With People!";
            _Ai_text.Font = font;
            _Ai_text.Color = new Color(Color.White);
            _Ai_text.Position = new Vector2f(250,250);
            _Ai_text.CharacterSize = 20;
            _Ai_text.DisplayedString = "Press W to Play With Ai!";
            _title.Font = font;
            _title.Color = new Color(Color.White);
            _title.Position = new Vector2f(50,100);
            _title.CharacterSize = 50;
            _title.DisplayedString = "Welcome to the Pong Game!";
        }



        public override void Render(RenderTarget target) {

            target.Draw(_play_text);
            target.Draw(_Ai_text);
            target.Draw(_title);
        }

        public override void update(float delta) {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q)) {

                _game.AddNomralPlayer();
                _game.ChangeState();

            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.W)) {
                _game.AddAiPlayer();
                _game.ChangeState();
            }
        }
    }
}
