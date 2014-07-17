package com.oncast.assessment.book.service;

import java.util.Comparator;

import com.oncast.assessment.book.Book;

public enum BookComparator implements Comparator<Book>{

	TITLE_ASC {
		public int compare(Book book1, Book book2) {
			return book1.getTitle().compareTo(book2.getTitle());
		}
	},

	TITLE_DESC {
		public int compare(Book book1, Book book2) {
			return (book1.getTitle().compareTo(book2.getTitle())) * -1;
		}
	},

	AUTHOR_ASC {
		public int compare(Book book1, Book book2) {
			return book1.getAuthor().compareTo(book2.getAuthor());
		}
	},

	AUTHOR_DESC {
		public int compare(Book book1, Book book2) {
			return (book1.getAuthor().compareTo(book2.getAuthor())) * -1;
		}
	},

	EDITION_ASC {
		public int compare(Book book1, Book book2) {
			return book1.getEdition().compareTo(book2.getEdition());
		}
	},

	EDITION_DESC {
		public int compare(Book book1, Book book2) {
			return (book1.getEdition().compareTo(book2.getEdition())) * -1;
		}
	};

	public static Comparator<Book> getComparator(final BookComparator[] rules) {
		return new Comparator<Book>() {
			public int compare(Book book1, Book book2) {
				for (BookComparator option : rules) {
					int result = option.compare(book1, book2);
					if (result != 0) {
						return result;
					}
				}
				return 0;
			}
		};
	}
}
