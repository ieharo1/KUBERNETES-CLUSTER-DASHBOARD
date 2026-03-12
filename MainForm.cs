using System;
using System.Threading.Tasks;
using System.Windows.Forms;

public class MainForm : Form
{
    private readonly ListView podsList;
    private readonly Label workersLabel;
    private readonly Label servicesLabel;
    private readonly Button refreshButton;
    private readonly KubernetesClient client;

    public MainForm()
    {
        Text = "Kubernetes Cluster Dashboard";
        Width = 900;
        Height = 600;

        client = new KubernetesClient("http://localhost:8001");

        podsList = new ListView
        {
            View = View.Details,
            Dock = DockStyle.Fill
        };
        podsList.Columns.Add("Pod", 350);
        podsList.Columns.Add("Namespace", 150);
        podsList.Columns.Add("Status", 120);

        workersLabel = new Label { Text = "Workers: -", Dock = DockStyle.Top, Height = 30 };
        servicesLabel = new Label { Text = "Servicios: -", Dock = DockStyle.Top, Height = 30 };
        refreshButton = new Button { Text = "Refrescar", Dock = DockStyle.Top, Height = 35 };
        refreshButton.Click += async (_, _) => await RefreshData();

        Controls.Add(podsList);
        Controls.Add(refreshButton);
        Controls.Add(servicesLabel);
        Controls.Add(workersLabel);

        Shown += async (_, _) => await RefreshData();
    }

    private async Task RefreshData()
    {
        try
        {
            podsList.Items.Clear();
            var pods = await client.GetPodsAsync();
            foreach (var pod in pods)
            {
                var item = new ListViewItem(new[] { pod.Name, pod.Namespace, pod.Status });
                podsList.Items.Add(item);
            }

            workersLabel.Text = $"Workers: {await client.GetWorkerCountAsync()}";
            servicesLabel.Text = $"Servicios: {await client.GetServiceCountAsync()}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

