using Microsoft.Phone.Tasks;

namespace XamlActions.Tasks {
    public class ReviewTaskWp {
        public void Show() {
            var task = new MarketplaceReviewTask();
            task.Show();
        }
    }
}