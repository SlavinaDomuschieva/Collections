using NUnit.Framework;
using System;
using System.Linq;

namespace Collections.Tests
{
    public class CollectionsTests
    {

        [Test]
        public void EmptyConstructorTest()
        {
            var nums = new Collection<int>();

            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void CollectionsConstructorSingleItemTest()
        {
            var nums = new Collection<int>(5);

            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
        }

        [Test]
        public void CollectionsConstructorMultipleItemsTest()
        {
            var nums = new Collection<int>(5, 10, 15);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15]"));
        }
        [Test]
        public void CollectionsCountAndCapacityTest()
        {
            var nums = new Collection<int>();
            
            int initialCapacity = nums.Capacity;

            for (int i = 0; i < initialCapacity; i++)
            {
                nums.Add(i);
                int test = nums.Capacity;
            }

            int afterCount = nums.Count;
            int afterCapacity = nums.Count;
            
            Assert.That(afterCount >= afterCapacity);
        }

        [Test]
        public void CollectionsAddItemTest()
        {
            var nums = new Collection<int>();
            nums.Add(5);
            nums.Add(6);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 6]"));
        }

        [Test]
        public void CollectionsAddRangeTest()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1, 10).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));

        }

        [Test]
        public void CollectionsAddRangeWithGrowTest()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Count, Is.GreaterThanOrEqualTo(nums.Count));

        }

        [Test]
        public void CollectionsGetByIndexTest()
        {
            var names = new Collection<string>("Peter", "Maria");
            var itemAt0 = names[0];
            var itemAt1 = names[1];
            Assert.That(itemAt0, Is.EqualTo("Peter"));
            Assert.That(itemAt1, Is.EqualTo("Maria"));

        }

        [Test]
        public void CollectionsGetByInvalidIndexTest()
        {
            var names = new Collection<string>("Bob", "Joe");

            string name = "";

            Assert.That(() => { name = names[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { name = names[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { name = names[500]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());

        }

        [Test]
        public void CollectionsInsertAtEndTest()
        {
            var names = new Collection<string>("Bob", "Joe");

            int index = names.Count;
            names.InsertAt(index, "Kim");

            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe, Kim]")); ;
        }

        [Test]
        public void CollectionsInsertAtMiddleTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");

            int index = (int)names.Count / 2;
            names.InsertAt(index, "Kim");

            Assert.That(names.ToString(), Is.EqualTo("[Bob, Kim, Joe, Merry]")); ;
        }

        [Test]
        public void CollectionsInsertAtStartTest()
        {
            var names = new Collection<string>("Bob, Joe, Merry");
            names.InsertAt(0, "Kim");

            Assert.That(names.ToString(), Is.EqualTo("[Kim, Bob, Joe, Merry]")); ;
        }

        [Test]
        public void CollectionsInsertAtInvalidIndexTest()
        {
            var names = new Collection<string>("Bob, Joe, Max");


            Assert.That(() => { names.InsertAt(-1, "Kim"); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names.InsertAt(3, "Kim"); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names.InsertAt(300, "Kim"); }, Throws.InstanceOf<ArgumentOutOfRangeException>());

        }

        [Test]
        public void CollectionsInsertAtWithGrowTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");
            int originalCount = names.Count;
            int originalCapacity = names.Capacity;
            int index = originalCount / 2;

            for (int i = 0; i < names.Capacity - originalCount; i++)
            {
                names.InsertAt(i, "kim" + i);
            }
            names.InsertAt(index, "Kim");


            Assert.That(names.Count, Is.GreaterThan(originalCount));
            Assert.That(names.Capacity, Is.GreaterThan(originalCapacity));
        }

        [Test]
        public void CollectionsClearTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");

            names.Clear();

            Assert.That(names.Count == 0);
            Assert.That(names.ToString(), Is.EqualTo("[]"));

        }

        [Test]
        public void CollectionsRemoveAtEndTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");
            int lastIndex = names.Count - 1;
            int originalCount = names.Count;
            names.RemoveAt(lastIndex);

            Assert.That(names.Count == originalCount - 1);
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));

        }

        [Test]
        public void CollectionsRemoveAtMiddleTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");
            int middleIndex = names.Count / 2;
            int originalCount = names.Count;
            names.RemoveAt(middleIndex);

            Assert.That(names.Count == originalCount - 1);
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Merry]"));

        }

        [Test]
        public void CollectionsRemoveAtStartTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");
            int originalCount = names.Count;
            names.RemoveAt(0);

            Assert.That(names.Count == originalCount - 1);
            Assert.That(names.ToString(), Is.EqualTo("[Joe, Merry]"));

        }

        [Test]
        public void CollectionsRemoveAtInvalidIndexTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");

            Assert.That(() => { names.RemoveAt(-1); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names.RemoveAt(3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names.RemoveAt(500); }, Throws.InstanceOf<ArgumentOutOfRangeException>());

        }

        [Test]
        public void CollectionsRemoveAllTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");
            int originalCount = names.Count;
            for (int i = originalCount - 1; i >= 0; i--)
            {
                names.RemoveAt(i);
            }
            Assert.That(names.Count == 0);
            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void ExchangeFirstLastTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");

            names.Exchange(0, names.Count - 1);

            Assert.That(names.ToString(), Is.EqualTo("[Merry, Joe, Bob]"));
        }

        [Test]
        public void ExchangeMiddleTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");

            names.Exchange(names.Count / 2, names.Count - 1);

            Assert.That(names.ToString(), Is.EqualTo("[Bob, Merry, Joe]"));
        }

        [Test]
        public void CollectionsExchangeInvalidIndexesTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");

            Assert.That(() => { names.Exchange(-1, 10); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void CollectionsToStringNestedCollectionsTest()
        {
            var names = new Collection<string>("Bob", "Joe", "Merry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();

            var nested = new Collection<object>(names, nums, dates);

            string nestedToString = nested.ToString();
            Assert.That(nestedToString, Is.EqualTo("[[Bob, Joe, Merry], [10, 20], []]"));

        }

        [Test]
        public void CollectionsToStringEmptyTest()
        {
            var nums = new Collection<int>();
           
           string numsToString = nums.ToString();
            Assert.That(numsToString, Is.EqualTo("[]"));

        }

        [Test]
        public void CollectionsToStringSingleTest()
        {
            var nums = new Collection<int>(10);

            string numsToString = nums.ToString();
            Assert.That(numsToString, Is.EqualTo("[10]"));

        }

        [Test]
        public void CollectionsToStringMultipleTest()
        {
            var nums = new Collection<int>(10, 20, 30);

            string numsToString = nums.ToString();
            Assert.That(numsToString, Is.EqualTo("[10, 20, 30]"));

        }

        [Test]
        public void CollectionsOneMillionItemsTest()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
           
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            
            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

    }
}
