using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Xunit;
using Moq;
using TicTacToe.View;
using TicTacToe.Model;

namespace TicTacToeTest
{
    public class ConsoleViewTest
    {
        private ConsoleView sut = new ConsoleView();

        [Fact]
        public void DisplayInstructionsShouldOutputCorrectString()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                string instructions = "Please choose a square to play on.";
                sut.DisplayInstructions(instructions);
        
                string result = sw.ToString();
                Assert.Contains(instructions, result);
            }
        }

        [Fact]
        public void DisplayBoardShouldDisplayBoardWhenEmpty()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                var stubBoard = new Mock<Board>();
                stubBoard.Setup(board => board.IsEmpty()).Returns(true);

                sut.DisplayBoard(stubBoard.Object);

                string expected = ConsoleView.emptyBoardString;
                string actual = sw.ToString();
                Assert.Contains(expected, actual);
            }
        }

        public void DisplayBoardShouldDisplayBoardWhenNotEmpty()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                var stubBoard = new Mock<Board>();
                stubBoard.Setup(board => board.IsEmpty()).Returns(false);
                List<Square> squares = GetFullCollectionOfSquares();
                squares.Find(x => x.Name == "A1").PlayOn();
                stubBoard.Setup(board => board.GetBoard()).Returns(squares);

                string expected = "X |  |  \n  |  |  \n  |  |  ";
                sut.DisplayBoard(stubBoard.Object);

                string actual = sw.ToString();
                Assert.Contains(expected, actual);
            }
        }

        [Fact]
        public void GetSquareToPlayOnShouldReturnChosenSquare()
        {
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader("a1"))
                {
                    var stubBoard = new Mock<Board>();
                    stubBoard.Setup(board => board.GetSquare("a1")).Returns(new Square("a1"));

                    Console.SetOut(sw);
                    Console.SetIn(sr);
            
                    Square square = sut.GetSquareToPlayOn(stubBoard.Object);
                    Assert.True(new Square("A1").Equals(square));
                }
            }
        }

        [Fact]
        public void GetSquareToPlayOnShouldPrintErrorMessageIfSquareDoesNotExist()
        {
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader("a1"))
                {
                    var mockBoard = new Mock<Board>();
                    mockBoard.Setup(board => board.GetSquare("a1")).Throws(new NoSuchSquareException());

                    Console.SetOut(sw);
                    Console.SetIn(sr);
            
                    Square square = sut.GetSquareToPlayOn(mockBoard.Object);
                    string result = sw.ToString();
                    Assert.Contains("Square does not exist!", result);
                }
            }
        }

        private List<Square> GetFullCollectionOfSquares()
        {
            List<Square> squares = new List<Square>();
            foreach (string squareValue in Enum.GetNames(typeof(SquareValue)))
            {
                squares.Add(new Square(squareValue));
            }
            return squares;
        }
    }
}
