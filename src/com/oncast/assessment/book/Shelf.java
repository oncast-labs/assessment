package com.oncast.assessment.book;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import com.oncast.assessment.book.exceptions.OrderingException;
import com.oncast.assessment.book.service.BookComparator;

import static com.oncast.assessment.book.service.BookComparator.*;

/**
 * Holds a bunch of books and the necessary operations
 * @author celso
 */
public class Shelf {
	
	private List<Book> booksOnTheShelf = new ArrayList<Book>();

	/**
	 * Add a new book to the shelf
	 * @param newBook the new book to be added
	 */
	public void addBook(Book newBook) {
		booksOnTheShelf.add(newBook);
	}
	
	public List<Book> orderBy(BookComparator ... rules) {
		if (rules == null) throw new OrderingException("Rules can't be null");
		List<Book> booksOrdered = new ArrayList<>(booksOnTheShelf);
		Collections.sort(booksOrdered, getComparator(rules));
		return booksOrdered;
	}

	public Shelf(Book bookJava, Book bookPatterns, Book bookHeadFirst, Book bookWeb) {
		addBook(bookJava);
		addBook(bookPatterns);
		addBook(bookHeadFirst);
		addBook(bookWeb);
	}

	public Shelf() { }
}
