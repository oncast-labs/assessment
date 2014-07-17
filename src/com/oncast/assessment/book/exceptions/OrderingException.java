package com.oncast.assessment.book.exceptions;

public class OrderingException extends RuntimeException {

	public OrderingException(String message) {
		super(message);
	}
	
	public OrderingException() {
		super();
	}

	private static final long serialVersionUID = -6141097546697487365L;

}
