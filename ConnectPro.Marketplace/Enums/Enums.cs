using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Enums
{
    public enum JobPostStatus
    {
        Open,
        Closed,
        Cancelled
    }

    public enum ApplicationStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public enum FeedItemType
    {
        JobPost,
        Service
    }

}
