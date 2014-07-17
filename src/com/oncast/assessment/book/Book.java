package com.oncast.assessment.book;

/**
 * Represents a single book
 * @author celso
 *
 */
public class Book {
	
	private String title;
	private String author;
	private Integer edition;

	public Book(String title, String author, int edition) {
		this.title = title;
		this.author = author;
		this.edition = edition;
	}

	public String getTitle() {
		return title;
	}

	public String getAuthor() {
		return author;
	}

	public Integer getEdition() {
		return edition;
	}
}
