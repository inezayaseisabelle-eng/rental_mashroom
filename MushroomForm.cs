namespace RentalAndMushroomApp
{
    /// <summary>
    /// Problem 2 - Mushroom guessing game.
    /// The user thinks of one of six mushrooms; the program asks at most THREE
    /// yes/no questions and names the mushroom.
    ///
    ///                     gills  forest  ring  convex
    /// Agaric jaunissant    yes    no      yes   yes
    /// Amanite tue-mouche   yes    yes     yes   yes
    /// Cepe de Bordeaux     no     yes     no    no
    /// Coprin chevelu       yes    no      yes   no
    /// Girolle              yes    yes     no    no
    /// Pied bleu            yes    yes     no    yes
    ///
    /// Decision tree:
    ///   Convex cup?
    ///     YES -> Ring?
    ///              NO  -> Pied bleu
    ///              YES -> Forest?  YES -> Amanite tue-mouche / NO -> Agaric jaunissant
    ///     NO  -> Forest?
    ///              NO  -> Coprin chevelu
    ///              YES -> Gills?   YES -> Girolle / NO -> Cepe de Bordeaux
    /// </summary>
    public class MushroomForm : Form
    {
        private const string QGills = "Does your mushroom have gills?";
        private const string QForest = "Does your mushroom grow in a forest?";
        private const string QRing = "Does your mushroom have a ring?";
        private const string QConvex = "Does your mushroom have a convex cup?";

        private readonly Label lblQuestion = new Label();
        private readonly Label lblStep = new Label();
        private readonly Button btnYes = new Button();
        private readonly Button btnNo = new Button();
        private readonly Button btnRestart = new Button();
        private readonly ListBox lstHistory = new ListBox();
        private readonly Label lblResult = new Label();

        // Node of the decision tree: either a question or a final answer.
        private class Node
        {
            public string Question;
            public string Answer;
            public Node Yes;
            public Node No;
            public bool IsLeaf => Answer != null;
        }

        private readonly Node root;
        private Node current;
        private int questionCount;

        public MushroomForm()
        {
            root = BuildTree();

            Text = "Mushroom Finder";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            ClientSize = new Size(480, 470);
            Font = new Font("Segoe UI", 10F);

            var lblTitle = new Label
            {
                Text = "Mushroom Finder",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 15)
            };

            var lblInfo = new Label
            {
                Text = "Think of one: Agaric jaunissant, Amanite tue-mouche, Cepe de Bordeaux,\n" +
                       "Coprin chevelu, Girolle, Pied bleu. Answer truthfully (max 3 questions).",
                AutoSize = true,
                Location = new Point(20, 48)
            };

            lblStep.AutoSize = true;
            lblStep.Location = new Point(20, 100);
            lblStep.ForeColor = Color.DimGray;

            lblQuestion.Location = new Point(20, 125);
            lblQuestion.Size = new Size(440, 30);
            lblQuestion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            btnYes.Text = "Yes";
            btnYes.Location = new Point(20, 165);
            btnYes.Size = new Size(120, 40);
            btnYes.Click += (s, e) => Answer(true);

            btnNo.Text = "No";
            btnNo.Location = new Point(155, 165);
            btnNo.Size = new Size(120, 40);
            btnNo.Click += (s, e) => Answer(false);

            btnRestart.Text = "Start again";
            btnRestart.Location = new Point(340, 165);
            btnRestart.Size = new Size(120, 40);
            btnRestart.Click += (s, e) => StartGame();

            var lblHistory = new Label { Text = "Your answers:", AutoSize = true, Location = new Point(20, 220) };
            lstHistory.Location = new Point(20, 245);
            lstHistory.Size = new Size(440, 100);

            lblResult.Location = new Point(20, 355);
            lblResult.Size = new Size(440, 50);
            lblResult.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblResult.ForeColor = Color.DarkGreen;

            var btnLogout = new Button { Text = "Logout", Location = new Point(360, 420), Size = new Size(100, 35) };
            btnLogout.Click += (s, e) => Close(); // LoginForm re-appears on FormClosed

            Controls.AddRange(new Control[]
            {
                lblTitle, lblInfo, lblStep, lblQuestion, btnYes, btnNo, btnRestart,
                lblHistory, lstHistory, lblResult, btnLogout
            });

            StartGame();
        }

        private static Node Leaf(string name) => new Node { Answer = name };

        private static Node BuildTree()
        {
            return new Node
            {
                Question = QConvex,
                Yes = new Node
                {
                    Question = QRing,
                    No = Leaf("Pied bleu"),
                    Yes = new Node
                    {
                        Question = QForest,
                        Yes = Leaf("Amanite tue-mouche"),
                        No = Leaf("Agaric jaunissant")
                    }
                },
                No = new Node
                {
                    Question = QForest,
                    No = Leaf("Coprin chevelu"),
                    Yes = new Node
                    {
                        Question = QGills,
                        Yes = Leaf("Girolle"),
                        No = Leaf("Cepe de Bordeaux")
                    }
                }
            };
        }

        private void StartGame()
        {
            current = root;
            questionCount = 0;
            lstHistory.Items.Clear();
            lblResult.Text = "";
            btnYes.Enabled = btnNo.Enabled = true;
            ShowCurrent();
        }

        private void ShowCurrent()
        {
            if (current.IsLeaf)
            {
                lblStep.Text = $"Found after {questionCount} question(s).";
                lblQuestion.Text = "";
                lblResult.Text = $"Your mushroom is: {current.Answer}";
                btnYes.Enabled = btnNo.Enabled = false;
            }
            else
            {
                lblStep.Text = $"Question {questionCount + 1} of at most 3";
                lblQuestion.Text = current.Question;
            }
        }

        private void Answer(bool yes)
        {
            if (current == null || current.IsLeaf) return;

            questionCount++;
            lstHistory.Items.Add($"{questionCount}. {current.Question}  ->  {(yes ? "Yes" : "No")}");
            current = yes ? current.Yes : current.No;
            ShowCurrent();
        }
    }
}
