using System;
using System.Collections.Generic;

namespace GNIS_MUL_Form.Models;

public partial class MeetingMinutesMasterTbl
{
    public int Id { get; set; }

    public string? CustomerType { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? Date { get; set; }

    public string? MeetingPlace { get; set; }

    public string? AttendsFromClientSide { get; set; }

    public string? AttendsFromHostSide { get; set; }

    public string? MeetingAgenda { get; set; }

    public string? MeetingDiscussion { get; set; }

    public string? MeetingDecision { get; set; }
}
