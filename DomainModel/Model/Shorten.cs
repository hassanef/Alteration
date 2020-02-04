using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Model
{
    public class Shorten: ValueObject
    {
        public short Left { get; private set; }
        public short Right{ get; private set; }

        private Shorten() { }

        public Shorten(short left, short right)
        {
            Left = (left > 5 || left < -5) ? throw new ArgumentException("left is more or less than valid range!"): left;
            Right = (right > 5 || right < -5) ? throw new ArgumentException("right is more or less than valid range!") : right; ;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Left;
            yield return Right;
        }
    }
}
