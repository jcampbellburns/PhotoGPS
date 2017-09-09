Public Class Project
    'This class allows us to save/load the current folder, locations, photo associations, and filtering so that we can stop and come back to it later

    Public Locations As IEnumerable(Of Location) = Enumerable.Empty(Of Location)
    Public Photos As IEnumerable(Of Photo) = Enumerable.Empty(Of Photo)

End Class
