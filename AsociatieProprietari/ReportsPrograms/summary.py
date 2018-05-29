#!/usr/bin/python3

from reportsmodule import *

SQLCommand = """SELECT Id, FORMAT(InvoiceDate,'yyyy-MM-dd') ,Value FROM InvoiceModels WHERE FlatId = '%s' ORDER BY Id"""

SQLCommand1 = """SELECT Id, FORMAT(PaymentDate,'yyyy-MM-dd') ,Value FROM PaymentsModels WHERE FlatId = '%s' ORDER BY Id"""

if len(sys.argv) > 1:
    account_id = sys.argv[1]
else:
    account_id = ""

print (str(sys.argv))

cursor.execute(SQLCommand % account_id) 

results = cursor.fetchone() 

# SCHIMBA PATHUL ASTA CATRE FOLDERUL TAU!!!
workbook = xlsxwriter.Workbook('D:\\AsociatieProprietari\\AsociatieProprietari\\ReportingFolder\\Summary_Rerpot.xlsx')
worksheet = workbook.add_worksheet()

# Set column width.
worksheet.set_column(0, 0, 12)
worksheet.set_column(1, 1, 12)
worksheet.set_column(2, 2, 12)
worksheet.set_column(3, 4, 12)
worksheet.set_column(4, 4, 12)
worksheet.set_column(5, 5, 12)
worksheet.set_column(6, 6, 12)
worksheet.set_column(7, 7, 12)
worksheet.set_column(8, 8, 20)
worksheet.set_column(9, 9, 20)
worksheet.set_column(10, 10, 20)

# Format
f_text_center_tahoma_8 = workbook.add_format(text_center_tahoma_8)
f_headers_format = workbook.add_format(headers_format)
f_text_left_tahoma_8 = workbook.add_format(text_left_tahoma_8)

# Header list.
headers = ['Invoice', 'Date', 'Value', '  ', 'Payment', 'Date', 'Value', '  ', 'Total Invoice Amount', 'Total Payment Amount', 'Remaining']

TotalInvoiceAmount = 0;
TotalPaymentAmount = 0;

# Writing header.
writeHeader(worksheet, 0, 0, headers, format=f_headers_format)

row = 1

while results:
    worksheet.write(row, 0, str(results[0]), f_text_center_tahoma_8)
    worksheet.write(row, 1, str(results[1]), f_text_center_tahoma_8)
    worksheet.write(row, 2, str(results[2]), f_text_left_tahoma_8)
    TotalInvoiceAmount += float(results[2])

    row += 1

    results = cursor.fetchone() 


cursor.execute(SQLCommand1 % account_id) 

results = cursor.fetchone() 

row = 1

while results:
    worksheet.write(row, 4, str(results[0]), f_text_center_tahoma_8)
    worksheet.write(row, 5, str(results[1]), f_text_center_tahoma_8)
    worksheet.write(row, 6, str(results[2]), f_text_left_tahoma_8)
    TotalPaymentAmount += float(results[2])

    row += 1

    results = cursor.fetchone() 

worksheet.write(1, 8, str(TotalInvoiceAmount), f_text_left_tahoma_8)
worksheet.write(1, 9, str(TotalPaymentAmount), f_text_left_tahoma_8)
worksheet.write(1, 10, str(TotalInvoiceAmount - TotalPaymentAmount), f_text_left_tahoma_8)

connection.close()

workbook.close()