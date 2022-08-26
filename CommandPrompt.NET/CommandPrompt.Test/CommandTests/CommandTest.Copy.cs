namespace CommandPrompt.Test.CommandTests;

public partial class CommandTest
{
    [Test]
    public void CopyCommand_SetNewName_PreviousNameNotChanged()
    {
        var copyOfCommand = _command.Copy();

        copyOfCommand.Name = "changed";

        Assert.Multiple(() =>
        {
            Assert.That(_command.Name, Is.EqualTo("Hello"));
            Assert.That(copyOfCommand.Name, Is.EqualTo("changed"));
            Assert.That(ReferenceEquals(_command, copyOfCommand), Is.False);
        });
    }

    [Test]
    public void CopyCommand_RemoveOverloads_PreviousOverloadsNotChanged()
    {
        var copyOfCommand = _command.Copy();

        copyOfCommand.Overloads.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(_command.Overloads, Has.Count.EqualTo(2));
            Assert.That(copyOfCommand.Overloads, Has.Count.EqualTo(0));
        });
    }

    [Test]
    public void CopyCommand_RemoveInners_PreviousInnersNotChanged()
    {
        var copyOfCommand = _command.Copy();

        copyOfCommand.InnerComands.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(_command.InnerComands, Has.Count.EqualTo(1));
            Assert.That(copyOfCommand.InnerComands, Has.Count.EqualTo(0));
        });
    }
}
