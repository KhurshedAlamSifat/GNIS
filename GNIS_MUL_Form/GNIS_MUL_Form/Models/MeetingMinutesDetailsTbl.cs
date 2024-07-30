using System;
using System.Collections.Generic;

namespace GNIS_MUL_Form.Models;

public partial class MeetingMinutesDetailsTbl
{
    public int Id { get; set; }

    public int? MeetingMasterId { get; set; }

    public int? ProductServiceId { get; set; }

    public int? Quantity { get; set; }

    public string? Unit { get; set; }
}
