# 🖥️ Kubernetes Cluster Dashboard

Aplicacion Windows Forms para monitorear un cluster Kubernetes.

---

## ✅ Descripcion

Dashboard de escritorio que consume la API de Kubernetes y muestra estado de pods, servicios y workers.

### ¿Que hace este proyecto?

- **Dashboard**: Resumen del cluster
- **Pods activos**: Lista de pods y estado
- **Workers**: Numero de nodos activos
- **Servicios**: Cantidad de servicios desplegados

---

## ✨ Caracteristicas Principales

| Caracteristica | Descripcion |
|----------------|-------------|
| **Windows Forms** | App de escritorio |
| **Kubernetes API** | Consumo via HTTP |
| **Vista de pods** | Estado y namespace |
| **Metricas basicas** | Servicios y workers |

---

## 🛠️ Stack Tecnologico

- **C# / .NET 8**
- **Windows Forms**
- **Kubernetes API**

---

## 📦 Instalacion y Uso

1) Ejecutar proxy de Kubernetes:

```bash
kubectl proxy
```

2) Abrir el proyecto en Visual Studio y ejecutar.

---

## 🗂️ Estructura del Proyecto

```
kubernetes-cluster-dashboard
├── KubernetesClusterDashboard.csproj
├── Program.cs
├── MainForm.cs
├── KubernetesClient.cs
└── README.md
```

---

© 2026 Isaac Esteban Haro Torres - Todos los derechos reservados.

