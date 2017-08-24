Imports System.Runtime.CompilerServices

Module IOFileinfoExtensions

    ''' <summary>
    ''' Safely rename or move a file.
    ''' </summary>
    ''' <param name="file">The file to be renamed or moved.</param>
    ''' <param name="destFileName">The proposed file name to which <paramref name="file"/> is to be renamed or moved.</param>
    ''' <param name="Overwrite">If <c>True</c>, <paramref name="destFileName"/> will be overwritten if it exists. If <c>False</c>, the file will be renamed to include a number. This parameter is not used if <paramref name="destFileName"/> references a file which does not exist yet.</param>
    ''' <returns>The filename and path <paramref name="file"/> has after this method returns. If <paramref name="destFileName"/> existed prior to <paramref name="file"/> being renamed or moved and <paramref name="Overwrite"/> was set to <c>False</c>, this value will differ from <paramref name="destFileName"/>.</returns>
    <Extension> Public Function MoveTo(file As IO.FileInfo, destFileName As String, Overwrite As Boolean) As String
        Dim proposedFileName = destFileName


        If (proposedFileName <> file.FullName) Then
            If IO.File.Exists(proposedFileName) Then
                If Overwrite Then
                    'overwrite
                    IO.File.Delete(proposedFileName)
                Else
                    'safely rename
                    Dim folder = IO.Path.GetDirectoryName(proposedFileName)
                    Dim filename = IO.Path.GetFileNameWithoutExtension(proposedFileName)
                    Dim ext = IO.Path.GetExtension(proposedFileName)

                    Dim i As UInteger = 2
                    While IO.File.Exists(proposedFileName)
                        proposedFileName = folder & IO.Path.DirectorySeparatorChar & filename & " (" & i.ToString & ")" & ext
                        i += 1
                    End While

                End If
            End If

            file.MoveTo(proposedFileName)

        End If

        Return proposedFileName
    End Function
End Module


Module PointLatLngExtensions
    <Extension()>
    Public Function DistanceTo(Start As PointLatLng, Destination As PointLatLng, Optional Units As DistanceToUnits = DistanceToUnits.Meters) As Double
        Const METERSPERKILOMETER As Integer = 1000
        Const FEETPERMETER As Double = 3.28084
        Const FEETPERYARD As Integer = 3
        Const FEETPERMILE As Integer = 5280

        Dim distancemeters = distVincenty(Start.Lat, Start.Lng, Destination.Lat, Destination.Lng)

        Select Case Units
            Case DistanceToUnits.Feet
                Return distancemeters * FEETPERMETER
            Case DistanceToUnits.Kilometers
                Return distancemeters / METERSPERKILOMETER
            Case DistanceToUnits.Meters
                Return distancemeters
            Case DistanceToUnits.Miles
                Return (distancemeters * FEETPERMETER) / FEETPERMILE
            Case DistanceToUnits.Yards
                Return (distancemeters * FEETPERMETER) / FEETPERYARD
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function
    Public Enum DistanceToUnits
        Meters = 0
        Kilometers = 1 '0.001
        Feet = 2
        Yards = 3
        Miles = 4
    End Enum

    Private Function distVincenty(ByVal lat1 As Double, ByVal lon1 As Double, ByVal lat2 As Double, ByVal lon2 As Double) As Double
        'INPUTS: Latitude and Longitude of initial and 
        '           destination points in decimal format.
        'OUTPUT: Distance between the two points in Meters.
        '
        '======================================
        ' Calculate geodesic distance (in m) between two points specified by
        ' latitude/longitude (in numeric [decimal] degrees)
        ' using Vincenty inverse formula for ellipsoids
        '======================================
        ' Code has been ported by lost_species from www.aliencoffee.co.uk to VBA
        ' from javascript published at:
        ' http://www.movable-type.co.uk/scripts/latlong-vincenty.html
        ' * from: Vincenty inverse formula - T Vincenty, "Direct and Inverse Solutions
        ' *       of Geodesics on the Ellipsoid with application
        ' *       of nested equations", Survey Review, vol XXII no 176, 1975
        ' *       http://www.ngs.noaa.gov/PUBS_LIB/inverse.pdf
        'Additional Reference: http://en.wikipedia.org/wiki/Vincenty%27s_formulae
        '======================================
        ' Copyright lost_species 2008 LGPL 
        ' http://www.fsf.org/licensing/licenses/lgpl.html
        '======================================
        ' Code modifications to prevent "Formula Too Complex" errors
        ' in Excel (2010) VBA implementation
        ' provided by Jerry Latham, Microsoft MVP Excel Group, 2005-2011
        ' July 23 2011
        '======================================

        Dim low_a As Double
        Dim low_b As Double
        Dim f As Double
        Dim L As Double
        Dim U1 As Double
        Dim U2 As Double
        Dim sinU1 As Double
        Dim sinU2 As Double
        Dim cosU1 As Double
        Dim cosU2 As Double
        Dim lambda As Double
        Dim lambdaP As Double
        Dim iterLimit As Integer
        Dim sinLambda As Double
        Dim cosLambda As Double
        Dim sinSigma As Double
        Dim cosSigma As Double
        Dim sigma As Double
        Dim sinAlpha As Double
        Dim cosSqAlpha As Double
        Dim cos2SigmaM As Double
        Dim C As Double
        Dim uSq As Double
        Dim upper_A As Double
        Dim upper_B As Double
        Dim deltaSigma As Double
        Dim s As Double ' final result, will be returned rounded to 3 decimals (mm).
        'added by JLatham to break up "Too Complex" formulas
        'into pieces to properly calculate those formulas as noted below
        'and to prevent overflow errors when using
        'Excel 2010 x64 on Windows 7 x64 systems
        Dim P1 As Double ' used to calculate a portion of a complex formula
        Dim P2 As Double ' used to calculate a portion of a complex formula
        Dim P3 As Double ' used to calculate a portion of a complex formula

        'See http://en.wikipedia.org/wiki/World_Geodetic_System
        'for information on various Ellipsoid parameters for other standards.
        'low_a and low_b in meters
        ' === GRS-80 ===
        ' low_a = 6378137
        ' low_b = 6356752.314245
        ' f = 1 / 298.257223563
        '
        ' === Airy 1830 ===  Reported best accuracy for England and Northern Europe.
        ' low_a = 6377563.396
        ' low_b = 6356256.910
        ' f = 1 / 299.3249646
        '
        ' === International 1924 ===
        ' low_a = 6378388
        ' low_b = 6356911.946
        ' f = 1 / 297
        '
        ' === Clarke Model 1880 ===
        ' low_a = 6378249.145
        ' low_b = 6356514.86955
        ' f = 1 / 293.465
        '
        ' === GRS-67 ===
        ' low_a = 6378160
        ' low_b = 6356774.719
        ' f = 1 / 298.247167

        '=== WGS-84 Ellipsoid Parameters ===
        low_a = 6378137       ' +/- 2m
        low_b = 6356752.3142
        f = 1 / 298.257223563
        '====================================
        L = toRad(lon2 - lon1)
        U1 = Math.Atan((1 - f) * Math.Tan(toRad(lat1)))
        U2 = Math.Atan((1 - f) * Math.Tan(toRad(lat2)))
        sinU1 = Math.Sin(U1)
        cosU1 = Math.Cos(U1)
        sinU2 = Math.Sin(U2)
        cosU2 = Math.Cos(U2)

        lambda = L
        lambdaP = 2 * Math.PI
        iterLimit = 100 ' can be set as low as 20 if desired.

        While (Math.Abs(lambda - lambdaP) > Double.Epsilon) And (iterLimit > 0)
            iterLimit = iterLimit - 1

            sinLambda = Math.Sin(lambda)
            cosLambda = Math.Cos(lambda)
            sinSigma = Math.Sqrt(((cosU2 * sinLambda) ^ 2) + ((cosU1 * sinU2 - sinU1 * cosU2 * cosLambda) ^ 2))
            If sinSigma = 0 Then
                distVincenty = 0  'co-incident points
                Exit Function
            End If
            cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda
            sigma = Atan2(cosSigma, sinSigma)
            sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma
            cosSqAlpha = 1 - sinAlpha * sinAlpha

            If cosSqAlpha = 0 Then 'check for a divide by zero
                cos2SigmaM = 0 '2 points on the equator
            Else
                cos2SigmaM = cosSigma - 2 * sinU1 * sinU2 / cosSqAlpha
            End If

            C = f / 16 * cosSqAlpha * (4 + f * (4 - 3 * cosSqAlpha))
            lambdaP = lambda

            'the original calculation is "Too Complex" for Excel VBA to deal with
            'so it is broken into segments to calculate without that issue
            'the original implementation to calculate lambda
            'lambda = L + (1 - C) * f * sinAlpha *   (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma *   (-1 + 2 * (cos2SigmaM ^ 2))))
            'calculate portions
            P1 = -1 + 2 * (cos2SigmaM ^ 2)
            P2 = (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * P1))
            'complete the calculation
            lambda = L + (1 - C) * f * sinAlpha * P2

        End While

        'If iterLimit < 1 Then
        '    'MsgBox "iteration limit has been reached, something didn't work."
        '    Throw New Exception("iteration limit has been reached, something didn't work.")
        '    'Exit Function
        'End If

        uSq = cosSqAlpha * (low_a ^ 2 - low_b ^ 2) / (low_b ^ 2)

        'the original calculation is "Too Complex" for Excel VBA to deal with
        'so it is broken into segments to calculate without that issue
        'the original implementation to calculate upper_A
        'upper_A = 1 + uSq / 16384 * (4096 + uSq *     (-768 + uSq * (320 - 175 * uSq)))
        'calculate one piece of the equation
        P1 = (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)))
        'complete the calculation
        upper_A = 1 + uSq / 16384 * P1

        'oddly enough, upper_B calculates without any issues - JLatham
        upper_B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)))

        'the original calculation is "Too Complex" for Excel VBA to deal with
        'so it is broken into segments to calculate without that issue
        'the original implementation to calculate deltaSigma
        'deltaSigma = upper_B * sinSigma * (cos2SigmaM + upper_B / 4 *     (cosSigma * (-1 + 2 * cos2SigmaM ^ 2)       - upper_B / 6 * cos2SigmaM * (-3 + 4 * sinSigma ^ 2) *         (-3 + 4 * cos2SigmaM ^ 2)))
        'calculate pieces of the deltaSigma formula
        'broken into 3 pieces to prevent overflow error that may occur in
        'Excel 2010 64-bit version.
        P1 = (-3 + 4 * sinSigma ^ 2) * (-3 + 4 * cos2SigmaM ^ 2)
        P2 = upper_B * sinSigma
        P3 = (cos2SigmaM + upper_B / 4 * (cosSigma * (-1 + 2 * cos2SigmaM ^ 2) _
    - upper_B / 6 * cos2SigmaM * P1))
        'complete the deltaSigma calculation
        deltaSigma = P2 * P3

        'calculate the distance
        s = low_b * upper_A * (sigma - deltaSigma)
        'round distance to millimeters
        distVincenty = Math.Round(s, 3)

    End Function

    Private Function SignIt(Degree_Dec As String) As Double
        'Input:   a string representation of a lat or long in the
        '         format of 10° 27' 36" S/N  or 10~ 27' 36" E/W
        'OUTPUT:  signed decimal value ready to convert to radians
        '
        Dim decimalValue As Double
        Dim tempString As String
        tempString = UCase(Trim(Degree_Dec))
        decimalValue = Convert_Decimal(tempString)
        If Right(tempString, 1) = "S" Or Right(tempString, 1) = "W" Then
            decimalValue = decimalValue * -1
        End If
        SignIt = decimalValue
    End Function

    Private Function Convert_Degree(Decimal_Deg As Double) As String
        'source: http://support.microsoft.com/kb/213449
        '
        'converts a decimal degree representation to deg min sec
        'as 10.46 returns 10° 27' 36"
        '
        Dim degrees As String
        Dim minutes As String
        Dim seconds As String
        '        With Application
        'Set degree to Integer of Argument Passed
        degrees = Int(Decimal_Deg)
        'Set minutes to 60 times the number to the right
        'of the decimal for the variable Decimal_Deg
        minutes = (Decimal_Deg - degrees) * 60
        'Set seconds to 60 times the number to the right of the
        'decimal for the variable Minute
        seconds = Format(((minutes - Int(minutes)) * 60), "0")
        'Returns the Result of degree conversion
        '(for example, 10.46 = 10° 27' 36")
        Convert_Degree = " " & degrees & "° " & Int(minutes) & "' " _
         & seconds + Chr(34)
        '        End With
    End Function

    Private Function Convert_Decimal(Degree_Deg As String) As Double
        'source: http://support.microsoft.com/kb/213449
        ' Declare the variables to be double precision floating-point.
        ' Converts text angular entry to decimal equivalent, as:
        ' 10° 27' 36" returns 10.46
        ' alternative to ° is permitted: Use ~ instead, as:
        ' 10~ 27' 36" also returns 10.46
        Dim degrees As Double
        Dim minutes As Double
        Dim seconds As Double
        '
        'modification by JLatham
        'allow the user to use the ~ symbol instead of ° to denote degrees
        'since ~ is available from the keyboard and ° has to be entered
        'through [Alt] [0] [1] [7] [6] on the number pad.
        Degree_Deg = Replace(Degree_Deg, "~", "°")

        ' Set degree to value before "°" of Argument Passed.
        degrees = Val(Left(Degree_Deg, InStr(1, Degree_Deg, "°") - 1))
        ' Set minutes to the value between the "°" and the "'"
        ' of the text string for the variable Degree_Deg divided by
        ' 60. The Val function converts the text string to a number.
        minutes = Val(Mid(Degree_Deg, InStr(1, Degree_Deg, "°") + 2,
             InStr(1, Degree_Deg, "'") - InStr(1, Degree_Deg, "°") - 2)) / 60
        ' Set seconds to the number to the right of "'" that is
        ' converted to a value and then divided by 3600.
        seconds = Val(Mid(Degree_Deg, InStr(1, Degree_Deg, "'") +
           2, Len(Degree_Deg) - InStr(1, Degree_Deg, "'") - 2)) / 3600
        Convert_Decimal = degrees + minutes + seconds
    End Function

    Private Function toRad(ByVal degrees As Double) As Double
        toRad = degrees * (Math.PI / 180)
    End Function

    Private Function Atan2(ByVal X As Double, ByVal Y As Double) As Double
        ' code nicked from:
        ' http://en.wikibooks.org/wiki/Programming:Visual_Basic_Classic
        '  /Simple_Arithmetic#Trigonometrical_Functions
        ' If you re-use this watch out: the x and y have been reversed from typical use.
        If Y > 0 Then
            If X >= Y Then
                Atan2 = Math.Atan(Y / X)
            ElseIf X <= -Y Then
                Atan2 = Math.Atan(Y / X) + Math.PI
            Else
                Atan2 = Math.PI / 2 - Math.Atan(X / Y)
            End If
        Else
            If X >= -Y Then
                Atan2 = Math.Atan(Y / X)
            ElseIf X <= Y Then
                Atan2 = Math.Atan(Y / X) - Math.PI
            Else
                Atan2 = -Math.Atan(X / Y) - Math.PI / 2
            End If
        End If
    End Function
End Module

