package com.oncast.assessment.tests;

import static org.junit.Assert.assertEquals;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.junit.Rule;
import org.junit.Test;
import org.junit.rules.ExpectedException;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;

import com.oncast.assessment.book.Book;
import com.oncast.assessment.book.Shelf;
import com.oncast.assessment.book.exceptions.OrderingException;
import com.oncast.assessment.book.service.BookComparator;
import com.oncast.assessment.book.service.OrderingService;

@RunWith(JUnit4.class)
public class TestOrdering {
	
	private Book bookJava = new Book("Java How to Program", "Deitel & Deitel", 2007);
	private Book bookPatterns = new Book("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002);
	private Book bookHeadFirst = new Book("Head First Design Patterns", "Elisabeth Freeman", 2004);
	private Book bookWeb = new Book("Internet & World Wide Web: How to Program", "Deitel & Deitel", 2007);
	private Shelf bookShelf = new Shelf(bookJava, bookPatterns, bookHeadFirst, bookWeb);
	
	@Rule
    public ExpectedException thrown = ExpectedException.none();
	
	@Test
	public void titleAscendingWithOneBook() throws OrderingException, IOException{
		Shelf oneBookShelf = new Shelf();
		oneBookShelf.addBook(bookJava);
		List<Book> expected = new ArrayList<Book>();
		expected.add(bookJava);
		OrderingService service = OrderingService.getService("ordering_title.cfg");
		assertEquals(expected, service.order(oneBookShelf));
	}
	
	@Test
	public void titleAscendingWithFourBooks() throws IOException {
		List<Book> expected = new ArrayList<Book>();
		expected.add(bookHeadFirst);
		expected.add(bookWeb);
		expected.add(bookJava);
		expected.add(bookPatterns);
		OrderingService service = OrderingService.getService("ordering_title.cfg");
		assertEquals(expected, service.order(bookShelf));
	}
	
	@Test
	public void authorAscendingAndTitleDescending() throws IOException {
		List<Book> expected = new ArrayList<Book>();
		expected.add(bookJava);
		expected.add(bookWeb);
		expected.add(bookHeadFirst);
		expected.add(bookPatterns);
		
		OrderingService service = OrderingService.getService("ordering_author_title.cfg");
		assertEquals(expected, service.order(bookShelf));
	}
	
	@Test
	public void editionDescendingAuthorDescendingTitleAscending() throws IOException {
		List<Book> expected = new ArrayList<Book>();
		expected.add(bookWeb);
		expected.add(bookJava);
		expected.add(bookHeadFirst);
		expected.add(bookPatterns);
		
		OrderingService service = OrderingService.getService("ordering_edition_author_title.cfg");
		assertEquals(expected, service.order(bookShelf));
	}

	@Test
	public void nullRules() throws OrderingException {
		Shelf emptyBookShelf = new Shelf();
		thrown.expect(OrderingException.class);
		BookComparator[] comparator = null;
		emptyBookShelf.orderBy(comparator);
	}
	
	@Test
	public void emptyShelf() {
		Shelf emptyBookShelf = new Shelf();
		assertEquals(new ArrayList<>(), emptyBookShelf.orderBy(BookComparator.TITLE_DESC));
	}
}