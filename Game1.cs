using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using VisioForge.Shared.WindowsMediaLib;
using System.Collections.Generic;
using System.Linq;
using VisioForge.Shared.Helpers;

namespace GP01Week6Lab1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        
        SpriteBatch spriteBatch;
   
        private Texture2D background;
        private Texture2D player; 
        private Texture2D waitress;
        private Texture2D frame;
        private Texture2D dish; 
        private Texture2D disco;
        private int[,] xyArray = new int[10, 2] { { 100, 100 }, { 10, 60 }, {10, 120 }, { 10, 180 },
                                                 { 10, 240 }, { 10, 300 }, { 10,360 }, { 10,420 }, 
                                                 { 100, 250 }, { 100, 400 } };
        private int[,] directions = new int[10, 2] { { 0, 0 }, { 0, 0 }, {0, 0 }, { 0, 0 },
                                                 { 0, 0 }, { 0, 0 }, { 0,0 }, { 0,0 },
                                                 { 0, 0 }, { 0, 0 } };
        private int[,] oneEat = new int[10, 3] { { 0, 0 , 0 }, { 0, 0,0 }, {0, 0,0 }, { 0, 0,0 },
                                                 { 0, 0 ,0}, { 0, 0 ,0}, { 0,0 ,0}, { 0,0 ,0},
                                                 { 0, 0 ,0}, { 0, 0,0 } };

        private int gameSpeed = 20; 

        private static String[] names = { "osid", "jan", "mehmet", "ahmet", "sawsan", "fadi", "hadi", "raad", "wessam", "laith" };
        SpriteFont font;
        private Viewport leftViewport;
        private Viewport rightViewport;
        IList<string> strList = new List<string>();
        IList<string> TheyEatAlready = new List<string>();
        private bool flag = true;

        private bool flagForRandomWaitting = true;
        private bool flagForRandomDestination = true;
        private bool flagForListAdd = true;
        private bool doesEveryOneGetFood = false;

        private int maxborek = 3;
        private int minborek = 1;
        private int borek = 25;//25
        private bool isBorekDishHere = true;
        private int borekOnDish = 5;//5

        private int maxcake = 1;
        private int mincake = 1;
        private int cake = 10;//10
        private bool isCakeDishHere = true;
        private int cakeOnDish = 5;//5

        private int maxdrink = 3;
        private int mindrink = 3;
        private int drink = 25 ;//25
        private bool isThereAnyDrinkOnTable = true;
        private int drinkOnTable = 5;//5

        private bool isTheGameEnded = false; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600; 
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            
        }

      
        protected override void Initialize()
        {
 

            base.Initialize();
            leftViewport = new Viewport();
            leftViewport.X = 0;
            leftViewport.Y = 0;
            leftViewport.Width = 600;
            leftViewport.Height = 500;
            leftViewport.MinDepth = 0;
            leftViewport.MaxDepth = 1;

            rightViewport = new Viewport();
            rightViewport.X = 0;
            rightViewport.Y = 500;
            rightViewport.Width = 600;
            rightViewport.Height = 600;
            rightViewport.MinDepth = 0;
            rightViewport.MaxDepth = 1;
        }


        protected override void LoadContent()
        {
            /* loading images */
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            background = Content.Load<Texture2D>("floor"); 
            player = Content.Load<Texture2D>(@"player");
            waitress= Content.Load<Texture2D>(@"waitress");
            frame = Content.Load<Texture2D>(@"frame");
            font = Content.Load<SpriteFont>("File");
            disco = Content.Load<Texture2D>("disco");
            dish = Content.Load<Texture2D>("dish");
            callThreads();  
        }



        // ***********************   drawing cartoon characters and names  *********************************




        // ***********************   update function  *********************************



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>  ** we did not use this  function **
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && gameSpeed >5)
            {
                gameSpeed = gameSpeed - 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && gameSpeed < 150)
            {
                gameSpeed = gameSpeed + 5;
            }
          

            // TODO: Add your update logic here

            base.Update(gameTime);
        }  

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>  ************************ draw function ******** for drowing the images *****
        protected override void Draw(GameTime gameTime)
        {
            if (!isTheGameEnded)
            {
                GraphicsDevice.Clear(Color.White);
                graphics.GraphicsDevice.Viewport = leftViewport;

                spriteBatch.Begin();
                staticDraw();
                spriteBatch.End();
                for (int i = 0; i < 10; i++)
                {

                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, names[i], new Vector2(xyArray[i, 0], xyArray[i, 1] - 15), Color.Black);
                    spriteBatch.Draw(player, new Rectangle(xyArray[i, 0], xyArray[i, 1], 45, 45), Color.White);
                    spriteBatch.End();
                }
                graphics.GraphicsDevice.Viewport = rightViewport;

                int xxais = 5;
                int yaxis = 0;
                if (flagForListAdd)
                {
                    flagForListAdd = false;
                    foreach (string console in strList)
                    {
                        spriteBatch.Begin();
                        spriteBatch.DrawString(font, console, new Vector2(xxais, yaxis), Color.Black);
                        spriteBatch.End();
                        yaxis = yaxis + 15;
                    }

                    flagForListAdd = true;

                }


            }
            else
            {
                GraphicsDevice.Clear(Color.White);
                graphics.GraphicsDevice.Viewport = leftViewport;
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "The End", new Vector2(150, 150), Color.Black);
                
                int x = 50;
                int y = 150; 
                for(int i = 0; i < 10; i++)
                {
                    y = y + 30;
                    spriteBatch.DrawString(font, names[i]+ "   Number of Cake : " + oneEat[i,0]+ "   Number of Drink: " + oneEat[i, 1] + "  Number of Borek:  " + oneEat[i, 2] , new Vector2(x, y), Color.Black);

                }
                spriteBatch.End();

            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected void control()
        {
           
            while (true)
            {
                if(TheyEatAlready.Count == 10)
                {
                    
                    doesEveryOneGetFood = true;
                    maxborek = 5;
                    maxcake = 2;
                    maxdrink = 5;
                    mindrink = 0;
                    minborek = 0;
                    mincake = 0;
                    
                    Thread.Sleep(100);
                    break;
                }
            }
            while (true)
            {
                if (borek == 0 && drink == 0 && cake == 0 && cakeOnDish == 0 && drinkOnTable == 0 && borekOnDish == 0)
                {
                    Console.WriteLine("heree");
                    isTheGameEnded = true;
                    break;
                }
            }
        }





        // ***********************   The waiter Function is responsable for fulling up the dishes.  *********************************


        protected void waiter()
        {
            while (true)
            {
                if(borekOnDish == 0 || borekOnDish ==1 && isBorekDishHere && borek > 0)
                {
                    isBorekDishHere = false;
                    if (borek >= 5)
                    {
                        int AmountToAdd = 5 - borekOnDish;
                        borek = borek - AmountToAdd;
                        borekOnDish = borekOnDish + AmountToAdd;
                    }
                    else
                    {
                        borekOnDish = borekOnDish + borek;
                        borek = 0; 
                    }
                    Thread.Sleep(100);
                    isBorekDishHere = true;
                }
                if (cakeOnDish == 0 || cakeOnDish == 1 && isCakeDishHere && cake > 0 )
                {
                    isCakeDishHere = false;

                    if(cake >= 5)
                    {
                        int AmountToAdd = 5 - cakeOnDish;
                        cake = cake - AmountToAdd;
                        cakeOnDish = cakeOnDish + AmountToAdd;
                    }
                    else
                    {
                        cakeOnDish = cakeOnDish + cake;
                        cake = 0; 
                    }
                    Thread.Sleep(100);
                    isCakeDishHere = true;
                }
                if (drinkOnTable == 0 || drinkOnTable == 1 && isThereAnyDrinkOnTable && drink > 0 )
                {
                    isThereAnyDrinkOnTable = false;
                    if (drink >= 5)
                    {
                        int AmountToAdd = 5 - drinkOnTable;
                        drink = drink - AmountToAdd;
                        drinkOnTable = drinkOnTable + AmountToAdd;
                    }
                    else
                    {
                        drinkOnTable = drinkOnTable + drink;
                        drink = 0;
                    }
                    Thread.Sleep(100);
                    isThereAnyDrinkOnTable = true;
                }

                Thread.Sleep(100);
            }
            

        }



        // ***********************   The brain function that give a decision for where to go .  *********************************


        protected void theBrain(int index)
        {
            

            



            int lastVisit = 0;
            int rendomTime;
            while (true)
            {
                if (flagForRandomWaitting)
                {
                    flagForRandomWaitting = false;
                    rendomTime = RandomNumber(125, 1500);
                    Thread.Sleep(100);
                    flagForRandomWaitting = true;
                    break;
                }
                else
                {
                    Thread.Sleep(150);
                }
            }
           
            Thread.Sleep(rendomTime);
            while (true)
            {
                int rendom;

                while (true)
                {
                    
                    if (flagForRandomDestination)
                    {
                        flagForRandomDestination = false;
                        rendom = RandomNumber(1, 5);
                        Thread.Sleep(100);
                        flagForRandomDestination = true;
                        break;
                    }
                    else
                    {
                        int wait;
                        flagForRandomWaitting = false;
                        wait = RandomNumber(125, 1500);
                        flagForRandomWaitting = true;
                        Thread.Sleep(wait);
                    }
                }

               
               

                
                switch (rendom)
                {
                    case 1:
                        if(lastVisit != 1)
                        {
                            if (doesEveryOneGetFood)
                            {
                                goingToBufe(xyArray[index, 0], xyArray[index, 1], index);
                            }
                                    
                        }
                                    
                        break;
                    case 2:
                        if (lastVisit != 2)
                        {
                            goingTodesco(xyArray[index, 0], xyArray[index, 1], index);
                        }
                        break;
                    case 3:
                        if (lastVisit != 3)
                        {
                            getingConvesationWithFreinds(xyArray[index, 0], xyArray[index, 1], index);
                        }
                        break;
                    case 4:
                        if (lastVisit != 4)
                        {
                            playingPlayStation(xyArray[index, 0], xyArray[index, 1], index);
                        }

                        break;
                  
                    default:
                        break;
                }
                
                if (!doesEveryOneGetFood)
                {
                    if (!TheyEatAlready.Contains(names[index]))
                    {
                        goingToBufe(xyArray[index, 0], xyArray[index, 1], index);
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                    
                   
                }
                lastVisit = rendom; 

            }
          

        }



        //***********************************   goingToBufe function that responsable for changing coordinates for spacific character  ***********************************  


        protected void goingToBufe(int x, int y, int index)
        {




            int xDis = 460;
            int yDis = 200;
            bool isThereAlreadyOne = false;
            while (true)
            {
                if (flag)
                {
                    flag = false;
                    for (int i = 0; i < 10; i++)
                    {
                        if (directions[i, 0] == xDis && directions[i, 1] == yDis)
                        {
                            isThereAlreadyOne = true;
                            break;
                        }
                    }

                    if (!isThereAlreadyOne)
                    {

                        directions[index, 0] = xDis;
                        directions[index, 1] = yDis;
                        isThereAlreadyOne = false;
                        flag = true;
                        break;
                    }
                    else
                    {

                        isThereAlreadyOne = false;
                        flag = true;
                        return;
                    }


                }
                else
                {
                    Thread.Sleep(200);
                }

            }


            // for print the massage .. 
            String printString = names[index] + ": goes to bufe";
            while (true)
            {
                if (flagForListAdd)
                {

                    flagForListAdd = false;
                    Thread.Sleep(100);
                    if (strList.Count == 6)
                    {
                        strList.RemoveAt(0);

                    }

                    strList.Add(printString);
                    Thread.Sleep(100);
                    flagForListAdd = true;
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }


            }
            
            int distanceX;
            int distanceY;

            while (true)
            {
                distanceX = xDis - xyArray[index, 0];
                distanceY = yDis - xyArray[index, 1];

                if (distanceX > 0)
                {
                    xyArray[index, 0] = xyArray[index, 0] + 1;

                }
                else if (distanceX < 0)
                {
                    xyArray[index, 0] = xyArray[index, 0] - 1;
                }

                if (distanceY > 0)
                {
                    xyArray[index, 1] = xyArray[index, 1] + 1;
                }
                else if (distanceY < 0)
                {
                    xyArray[index, 1] = xyArray[index, 1] - 1;
                }
                if (distanceY == 0 && distanceX == 0)
                {
                    break;
                }
                Thread.Sleep(gameSpeed);
            }

            int cakeToEat = 0;
            int borekToEat = 0 ;
            int drinkToDrink = 0 ;

            while (true)
            {
                cakeToEat = RandomNumber(mincake, maxcake + 1);
                borekToEat = RandomNumber(minborek, maxborek + 1);
                drinkToDrink = RandomNumber(mindrink, maxdrink + 1);

                if (cakeToEat != 0 && borekToEat != 0 && drinkToDrink != 0)
                    break;

               
            }

            while (true)
            {
                if (isBorekDishHere && borekOnDish >= borekToEat)
                {
                    isBorekDishHere = false;
                    borekOnDish = borekOnDish - borekToEat;
                    Thread.Sleep(100);
                    isBorekDishHere = true;
                    break;
                }
                else if (isBorekDishHere && borekOnDish < borekToEat && borekOnDish != 0)
                {
                    borekToEat = borekOnDish;
                    isBorekDishHere = false;
                    borekOnDish = 0;
                    Thread.Sleep(100);
                    isBorekDishHere = true;
                    break;
                }
                else if (!isBorekDishHere)
                {
                    Thread.Sleep(20);
                }
                else if (borekOnDish == 0)
                {
                    Thread.Sleep(30);
                    if(borekOnDish == 0)
                    {
                        borekToEat = 0;
                    }
                    else
                    {
                        borekOnDish = borekOnDish - borekToEat;
                    }
                   

                    break;
                }

            }

            while (true)
            {
                if (isThereAnyDrinkOnTable && drinkOnTable >= drinkToDrink)
                {
                    isThereAnyDrinkOnTable = false;
                    drinkOnTable = drinkOnTable - drinkToDrink;
                    Thread.Sleep(100);
                    isThereAnyDrinkOnTable = true;
                    break;
                }
                else if (isThereAnyDrinkOnTable && drinkOnTable < drinkToDrink && drinkOnTable != 0)
                {
                    drinkToDrink = drinkOnTable;
                    isThereAnyDrinkOnTable = false;
                    drinkOnTable = 0;
                    Thread.Sleep(100);
                    isThereAnyDrinkOnTable = true;
                    break;
                }
                else if (!isThereAnyDrinkOnTable)
                {
                    Thread.Sleep(20);
                }
                else if (drinkOnTable == 0)
                {
                    Thread.Sleep(30);
                    if (drinkOnTable == 0)
                    {
                        drinkToDrink = 0;
                    }
                    else
                    {
                        drinkOnTable = drinkOnTable - drinkToDrink;
                    }


                    break;
                }

            }

            while (true)
            {
                if (isCakeDishHere && cakeOnDish >= cakeToEat)
                {
                    isCakeDishHere = false;
                    cakeOnDish = cakeOnDish - cakeToEat;
                    Thread.Sleep(100);
                    isCakeDishHere = true;
                    break;
                }
                else if (isCakeDishHere && cakeOnDish < cakeToEat && cakeOnDish != 0)
                {
                    cakeToEat = cakeOnDish;
                    isCakeDishHere = false;
                    cakeOnDish = 0;
                    Thread.Sleep(100);
                    isCakeDishHere = true;
                    break;
                }
                else if (!isCakeDishHere)
                {
                    Thread.Sleep(20);
                }
                else if (cakeOnDish == 0)
                {
                    Thread.Sleep(30);
                    if (cakeOnDish == 0)
                    {
                        cakeToEat = 0;
                    }
                    else
                    {
                        cakeOnDish = cakeOnDish - cakeToEat;
                    }


                    break;
                }
            }
           

            TheyEatAlready.Add(names[index]);

            oneEat[index, 0] = oneEat[index, 0] + cakeToEat;
            oneEat[index, 1] = oneEat[index, 1] + drinkToDrink;
            oneEat[index, 2] = oneEat[index, 2] + borekToEat;
            Console.WriteLine(cakeToEat + "  " + drinkToDrink + "  " + borekToEat);


        }

        
        //***********************************   goingTodesco function that responsable for changing coordinates for spacific character  ***********************************
        
        protected void goingTodesco(int x, int y, int index)
        {
            String printString = names[index] + ": goes to desco";
            while (true)
            {
                if (flagForListAdd)
                {

                    flagForListAdd = false;
                    Thread.Sleep(100);
                    if (strList.Count == 6)
                    {
                        strList.RemoveAt(0);
                       
                    }

                    strList.Add(printString);
                    Thread.Sleep(100);
                    flagForListAdd = true;
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }


            }

            int  randomx = RandomNumber(150,411);
            int  randomy = RandomNumber(10, 271);
            while (true)
            {
                if (flag)
                {
                    flag = false; 
                    directions[index, 0] = randomx;
                    directions[index, 1] = randomy;
                    for (int i = 0; i < 10; i++)
                    {
                        if (directions[i, 0] == randomx && directions[i, 1] == randomy && i != index)
                        {
                            randomx = RandomNumber(150, 411); ;
                            randomy = RandomNumber(10, 271);
                            i = 0;
                           
                        }

                    }
                    flag = true;
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                }

            }
           
           
           
            int randomTime = RandomNumber(5000, 15000);
            int distanceX  ;
            int distanceY  ;
          
            while (true)
            {
                distanceX = randomx - xyArray[index, 0];
                distanceY = randomy - xyArray[index, 1];

                if (distanceX > 0 )
                {
                    xyArray[index, 0] = xyArray[index, 0] + 1;

                }
                else if(distanceX < 0 )
                {
                    xyArray[index, 0] = xyArray[index, 0] - 1;
                }
                
                if (distanceY > 0 )
                {
                    xyArray[index, 1] = xyArray[index, 1] + 1;
                }
                else if (distanceY < 0)
                {
                    xyArray[index, 1] = xyArray[index, 1] - 1;
                }
                if(distanceY == 0 && distanceX == 0)
                {
                    break;
                }
                Thread.Sleep(gameSpeed);
            }

         

            Thread.Sleep(randomTime);
        }



        //***********************************   getingConvesationWithFreinds function that responsable for changing coordinates for spacific character  ***********************************

        protected void getingConvesationWithFreinds(int x, int y, int index)
        {
            String printString = names[index] + ": goes to freind";
            while (true)
            {
                if (flagForListAdd)
                {

                    flagForListAdd = false;
                    Thread.Sleep(100);
                    if (strList.Count == 6)
                    {
                        strList.RemoveAt(0);
                       
                    }
                    strList.Add(printString);
                    Thread.Sleep(100);
                    flagForListAdd = true;
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }


            }
            Console.WriteLine();
            int randomTime = RandomNumber(5000, 15000);
            int randomx =300;
            int randomy = 300;
            int distanceX;
            int distanceY;

            while (true)
            {
                distanceX = randomx - xyArray[index, 0];
                distanceY = randomy - xyArray[index, 1];

                if (distanceX > 0)
                {
                    xyArray[index, 0] = xyArray[index, 0] + 1;

                }
                else if (distanceX < 0)
                {
                    xyArray[index, 0] = xyArray[index, 0] - 1;
                }

                if (distanceY > 0)
                {
                    xyArray[index, 1] = xyArray[index, 1] + 1;
                }
                else if (distanceY < 0)
                {
                    xyArray[index, 1] = xyArray[index, 1] - 1;
                }
                if (distanceY == 0 && distanceX == 0)
                {
                    break;
                }
                Thread.Sleep(gameSpeed);
            }
            Thread.Sleep(randomTime);
        }

        //***********************************   playingPlayStation function that responsable for changing coordinates for spacific character  ***********************************

        protected void playingPlayStation(int x, int y, int index)
        {
            String printString = names[index] + ": goes to playstation";
            while (true)
            {
                if (flagForListAdd)
                {

                    flagForListAdd = false;
                    Thread.Sleep(100);
                    if (strList.Count() == 6)
                    {

                        strList.RemoveAt(0);
                        
                    }

                    strList.Add(printString);
                    Thread.Sleep(100);
                    flagForListAdd = true;
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }

                int randomx = 25;
                int randomy = 25;
                int distanceX;
                int distanceY;

                while (true)
                {
                    distanceX = randomx - xyArray[index, 0];
                    distanceY = randomy - xyArray[index, 1];

                    if (distanceX > 0)
                    {
                        xyArray[index, 0] = xyArray[index, 0] + 1;

                    }
                    else if (distanceX < 0)
                    {
                        xyArray[index, 0] = xyArray[index, 0] - 1;
                    }

                    if (distanceY > 0)
                    {
                        xyArray[index, 1] = xyArray[index, 1] + 1;
                    }
                    else if (distanceY < 0)
                    {
                        xyArray[index, 1] = xyArray[index, 1] - 1;
                    }
                    if (distanceY == 0 && distanceX == 0)
                    {
                        break;
                    }
                    Thread.Sleep(gameSpeed);
                }
            }
       
            int randomTime = RandomNumber(5000, 15000);
            Thread.Sleep(randomTime);
        }



         //*************************************     


        // Threads that every thread resposable for the coordinates for one character but the t1 thread calls updated for infinity    
        


       // ******************************************



        protected void callThreads()
        {
           


            Thread G1 = new Thread(() =>
            {
                theBrain(0);

            })
            { IsBackground = true };
            G1.Start();

            Thread G2 = new Thread(() =>
            {
                theBrain(1);

            })
            { IsBackground = true };
            G2.Start();

            Thread G3 = new Thread(() =>
            {
                theBrain(2);

            })
            { IsBackground = true };
            G3.Start();
            Thread G4 = new Thread(() =>
            {
                theBrain(3);

            })
            { IsBackground = true };
            G4.Start();

            Thread G5 = new Thread(() =>
            {
                theBrain(4);

            })
            { IsBackground = true };
            G5.Start();

            Thread G6 = new Thread(() =>
            {
                theBrain(5);

            })
            { IsBackground = true };
            G6.Start();

            Thread G7 = new Thread(() =>
            {
                theBrain(6);

            })
            { IsBackground = true };
            G7.Start();

            Thread G8 = new Thread(() =>
            {
                theBrain(7);

            })
            { IsBackground = true };
            G8.Start();

            Thread G9 = new Thread(() =>
            {
                theBrain(8);

            })
            { IsBackground = true };
            G9.Start();

            Thread G10 = new Thread(() =>
            {
                theBrain(9);

            })
            { IsBackground = true };
            G10.Start();

            Thread W = new Thread(() =>
            {
                waiter();

            })
            { IsBackground = true };
            W.Start();

            Thread controller = new Thread(() =>
            {
                control();

            })
            { IsBackground = true };
            controller.Start();
        }


        //***********************************    staticDraw funtion responsable for drawing static images (not moveable objects)   ***********************************


        protected void staticDraw()
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 600, 600), Color.White);
            spriteBatch.DrawString(font, "borek : " + borek.ToString(), new Vector2(370, 5), Color.Black);
            spriteBatch.DrawString(font, "cake : " + cake.ToString(), new Vector2(450, 5), Color.Black);
            spriteBatch.DrawString(font, "drink : " + drink.ToString(), new Vector2(530, 5), Color.Black);
            spriteBatch.DrawString(font, "borek on dish : " + borekOnDish.ToString(), new Vector2(450, 440), Color.Black);
            spriteBatch.DrawString(font, "cake on dish : " + cakeOnDish.ToString(), new Vector2(450, 460), Color.Black);
            spriteBatch.DrawString(font, "drink on dish : " + drinkOnTable.ToString(), new Vector2(450, 480), Color.Black);
            spriteBatch.Draw(waitress, new Rectangle(555, 250, 45, 45), Color.White);
            spriteBatch.Draw(frame, new Rectangle(480, 200, 80, 140), Color.White);
            spriteBatch.Draw(dish, new Rectangle(500, 205, 40, 40), Color.White);
            spriteBatch.Draw(dish, new Rectangle(500, 250, 40, 40), Color.White);
            spriteBatch.Draw(dish, new Rectangle(500, 295, 40, 40), Color.White);
            spriteBatch.DrawString(font, borekOnDish.ToString(), new Vector2(514, 216), Color.White);
            spriteBatch.DrawString(font, cakeOnDish.ToString(), new Vector2(514, 261), Color.White);
            spriteBatch.DrawString(font, drinkOnTable.ToString(), new Vector2(514, 306), Color.White);
            spriteBatch.Draw(disco, new Rectangle(140, 10, 270, 270), Color.White);
        }





        //************************** random function for returning random numbers *************************************



        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


    }
}
