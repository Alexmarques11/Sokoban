using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sokoban
{
    public class Player
    {
        // Current player position in the matrix (multiply by tileSize prior to drawing)
        private Point position; //Point = Vector2, mas são inteiros
        public Point Position => position; //auto função (equivalente a ter só get sem put)
        public Point Point => lastPosition;
        private bool keysReleased = true;
        private Game1 game;
        private Point lastPosition;

        public Player(Game1 game1, int x, int y) //constructor que dada a as +osições guarda a sua posição
        {
            position = new Point(x, y);
            game = game1;
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (keysReleased)
            {
                keysReleased = false;
                if (kState.IsKeyDown(Keys.A)) position.X--;
                else if (kState.IsKeyDown(Keys.W)) position.Y--;
                else if (kState.IsKeyDown(Keys.S)) position.Y++;
                else if (kState.IsKeyDown(Keys.D)) position.X++;
                else keysReleased = true;

                if (game.HasBox(position.X, position.Y))
                {
                    int deltaX = position.X - lastPosition.X;
                    int deltaY = position.Y - lastPosition.Y;
                    Point boxTarget = new Point(deltaX + position.X, deltaY + position.Y);
                    // se sim, caixa pode mover-se?
                    if (game.FreeTile(boxTarget.X, boxTarget.Y))
                    {
                        for (int i = 0; i < game.boxes.Count; i++)
                        {
                            if (game.boxes[i].X == position.X && game.boxes[i].Y == position.Y)
                            {
                                game.boxes[i] = boxTarget;
                            }
                        }
                    }
                    else
                    {
                        position = lastPosition;
                    }
                }
                else
                {
                    // se não é caixa, se não está livre, parado!
                    if (!game.FreeTile(position.X, position.Y))
                        position = lastPosition;
                }
            }
            else
            {
                if (kState.IsKeyUp(Keys.A) && kState.IsKeyUp(Keys.W) &&
                kState.IsKeyUp(Keys.S) && kState.IsKeyUp(Keys.D))
                {
                    keysReleased = true;
                }
            }
        }

    }
}
