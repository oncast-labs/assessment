using Assessment.Configuration;
using Assessment.Models;
using Assessment.Resources;
using Assessment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Assessment.Test
{
    [TestClass]
    public class Tests
    {
        #region Fields

        private List<Book> fBooks;

        private readonly string C_BOOK1 = "Java How to Program";
        private readonly string C_BOOK2 = "Patterns of Enterprise Application Architecture";
        private readonly string C_BOOK3 = "Head First Design Patterns";
        private readonly string C_BOOK4 = "Internet & World Wide Web: How to Program";

        #endregion

        #region Private methods

        private void AssertBookOrder(params string[] pSortedBookTitles)
        {
            Assert.AreEqual(fBooks.Count, pSortedBookTitles.Length, "Parameters' size differ");

            for (int vCount = 0; vCount < fBooks.Count; vCount++)
            {
                Book vBook = fBooks[vCount];

                Assert.IsNotNull(vBook, string.Format("Book {0} should not be null", vCount));
                Assert.AreEqual(vBook.Title, pSortedBookTitles[vCount], 
                    string.Format("Book {0} title is wrong: found {1}, should be {2}", vCount, vBook.Title, pSortedBookTitles[vCount]));
            }
        }

        #endregion

        #region Public methods

        [TestMethod]
        public void PreDefinedTests()
        {
            #region General testing (configuration) & setup

            OrderingConfiguration vOrderingConfiguration = new OrderingConfiguration();
            vOrderingConfiguration.AddOrder(OrderingConfigurations.TitleAscending);

            Assert.IsNotNull(vOrderingConfiguration, "Failed to create ordering configuration object");
            Assert.IsNotNull(vOrderingConfiguration.Orders, "Failed to create ordering configuration object's order list");

            List<Order> vOrders = vOrderingConfiguration.GetOrders();
            Assert.IsNotNull(vOrders, "Failed to get order list");
            Assert.AreEqual(vOrders.Count, 1, "Order list should contain 1 order");
            Assert.AreEqual(vOrders[0].PropertyName, "Title", "Order property name should be 'Title'");
            Assert.AreEqual(vOrders[0].PropertyType, typeof(string), "Order property type should be 'string'");
            Assert.AreEqual(vOrders[0].OrderingDirection, Direction.Ascending, "Order property direction should be 'Ascending'");

            BookComparer vBookComparer = new BookComparer();
            vBookComparer.Orders = vOrders;

            Shelf vShelf = new Shelf();
            vShelf.AddDefaultBooks();

            FileConfigurationLoader vFileConfigurationLoader = new FileConfigurationLoader();

            #endregion

            #region Test 1: Order rules = Title, Ascending

            vOrderingConfiguration.Clear();
            vFileConfigurationLoader.LoadConfigurationOrders(@"..\..\..\Assessment\Resources\ordering_AuthorAscending.cfg", vOrderingConfiguration);

            fBooks = vShelf.GetSortedBookList(vBookComparer);
            Assert.IsNotNull(fBooks, "Failed to create sorted book list");
            Assert.AreEqual(fBooks.Count, 4, "Sorted book list count should be 4");
            AssertBookOrder(C_BOOK3, C_BOOK4, C_BOOK1, C_BOOK2);

            #endregion

            #region Test 2: Order rules = Author, Ascending / Title, Descending

            vOrderingConfiguration.Clear();
            vFileConfigurationLoader.LoadConfigurationOrders(@"..\..\..\Assessment\Resources\ordering_AuthorAscending_TitleDescending.cfg", vOrderingConfiguration);

            vBookComparer.Orders = vOrderingConfiguration.GetOrders();

            fBooks = vShelf.GetSortedBookList(vBookComparer);
            Assert.IsNotNull(fBooks, "Failed to create sorted book list");
            Assert.AreEqual(fBooks.Count, 4, "Sorted book list count should be 4");
            AssertBookOrder(C_BOOK1, C_BOOK4, C_BOOK3, C_BOOK2);

            #endregion

            #region Test 3: Order rules = Edition, Descending / Author, Descending / Title, Ascending

            vOrderingConfiguration.Clear();
            vFileConfigurationLoader.LoadConfigurationOrders(@"..\..\..\Assessment\Resources\ordering_EditionDescending_AuthorDescending_TitleAscending.cfg", vOrderingConfiguration);

            vBookComparer.Orders = vOrderingConfiguration.GetOrders();

            fBooks = vShelf.GetSortedBookList(vBookComparer);
            Assert.IsNotNull(fBooks, "Failed to create sorted book list");
            Assert.AreEqual(fBooks.Count, 4, "Sorted book list count should be 4");
            AssertBookOrder(C_BOOK4, C_BOOK1, C_BOOK3, C_BOOK2);

            #endregion

            #region Test 4: Order rules = null

            vOrderingConfiguration.Clear();

            vBookComparer.Orders = null;

            fBooks = null;

            try
            {
                fBooks = vShelf.GetSortedBookList(vBookComparer);
                Assert.Fail("Shelf's 'GetSortedBookList' should have thrown an exception");
            }
            catch (OrderingException)
            {
                // Caught the expected exception
            }
            catch (Exception ex)
            {
                Assert.Fail("Caught an unexpeted exception: {0}", ex);
            }

            #endregion

            #region Test 5: Order rules = empty

            vOrderingConfiguration.Clear();
            
            vBookComparer.Orders = vOrderingConfiguration.GetOrders();

            fBooks = vShelf.GetSortedBookList(vBookComparer);
            Assert.IsNotNull(fBooks, "Failed to create sorted book list");
            Assert.AreEqual(fBooks.Count, 0, "Sorted book list count should be 0");

            #endregion
        }

        #endregion
    }
}
