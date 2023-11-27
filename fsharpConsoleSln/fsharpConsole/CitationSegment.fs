module CitationSegment

type CitationSegment =
    | BookAuthorSegment of name: string * authorId: string
    | TextSegment of text: string
    | BookTitleSegment of title: string * bookId: string
