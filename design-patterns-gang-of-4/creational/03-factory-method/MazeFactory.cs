using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.FactoryMethod
{
    interface IDoor
    {
    }
    interface IWall
    {
    }
    interface IMazeFactory
    {
        IDoor MakeDoor();
        IWall MakeWall();
    }

    class StandardMazeFactory : IMazeFactory
    {
        public IDoor MakeDoor()
        {
            throw new NotImplementedException();
        }

        public IWall MakeWall()
        {
            throw new NotImplementedException();
        }
    }

    class CountingItemsMazeFactory : IMazeFactory
    {
        public IDoor MakeDoor()
        {
            throw new NotImplementedException();
        }

        public IWall MakeWall()
        {
            throw new NotImplementedException();
        }
    }

    // But there might need to be behaviours across the factories

    interface ICard
    {
        bool PayFor(decimal amount);
    }

    interface ICardFactory
    {
        ICard GetCard();
    }

    class DebitCardMaker : ICardFactory
    {
        public ICard GetCard()
        {
            throw new NotImplementedException();
        }
    }

    class CreditCardMaker : ICardFactory
    {
        public ICard GetCard()
        {
            throw new NotImplementedException();
        }
    }

}
