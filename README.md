# BeThereOrBeSquare

little C# based console game

        ╔═╦═══════╗
        ║x║0123456║
        ╠═╬═══════╣
        ║0║       ║
        ║1║       ║
        ║2║       ║
        ║3║       ║
        ║4║       ║
        ║5║       ║
        ║6║       ║
        ╚═╩═══════╝

You and the computer take turns placing pieces on a 7 x 7 grid, and try not to form a square with your pieces.

        ╔═╦═══════╗
        ║x║0123456║
        ╠═╬═══════╣
        ║0║       ║
        ║1║ O O   ║
        ║2║       ║
        ║3║ O O   ║
        ║4║       ║
        ║5║       ║
        ║6║       ║
        ╚═╩═══════╝

In the above example, the Os form a square, and therefore the player placing them has lost

        ╔═╦═══════╗
        ║x║0123456║
        ╠═╬═══════╣
        ║0║  X    ║
        ║1║ X X   ║
        ║2║  X    ║
        ║3║       ║
        ║4║       ║
        ║5║       ║
        ║6║       ║
        ╚═╩═══════╝

Diagonal squares are also counted, so the player placing the Xs here has lost

You play your turn by writing a 2-digit string comprising the row and column number you want to place your piece on. For example, if you wish to place a piece on Row 0, Column 1, simply type "01" and press enter.

        ╔═╦═══════╗
        ║x║0123456║
        ╠═╬═══════╣
        ║0║X      ║
        ║1║       ║
        ║2║       ║
        ║3║       ║
        ║4║       ║
        ║5║       ║
        ║6║       ║
        ╚═╩═══════╝

(please note that in the actual program both the player and the computer's pieces are represented by the ● symbol of an appropriate color (red for the player and blue for the computer))
